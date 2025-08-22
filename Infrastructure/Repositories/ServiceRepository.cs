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
    }

}
