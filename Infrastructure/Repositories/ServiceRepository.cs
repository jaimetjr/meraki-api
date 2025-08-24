using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }

        public async Task<List<Service>> GetByCategoryAsync(string category) =>
            await _dbSet.Where(s => s.Category != null && s.Category.Name == category).ToListAsync();

        public override async Task<List<Service>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Category)
                               .Include(x => x.Benefits)
                               .ToListAsync();
        }

        public override async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(x => x.Category)
                               .Include(x => x.Benefits)
                               .FirstOrDefaultAsync(s => s.Id == id);
        }
    }

}
