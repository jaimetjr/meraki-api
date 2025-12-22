using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Service>> GetByCategoryAsync(string category) =>
            (await _dbSet
                .Include(s => s.Category)
                .Where(s => s.Category != null && s.Category.Name == category)
                .ToListAsync()).AsReadOnly();

        public override async Task<IReadOnlyCollection<Service>> GetAllAsync()
        {
            return (await _dbSet.Include(x => x.Category)
                               .Include(x => x.Benefits)
                               .ToListAsync()).AsReadOnly();
        }

        public override async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(x => x.Category)
                               .Include(x => x.Benefits)
                               .FirstOrDefaultAsync(s => s.Id == id);
        }
    }

}
