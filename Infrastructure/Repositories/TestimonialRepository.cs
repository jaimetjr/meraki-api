using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(AppDbContext context) : base(context) { }

        public async Task<List<Testimonial>> GetTopRatedAsync(int count)
        {
            return await _dbSet.OrderByDescending(t => t.Rating).Take(count).ToListAsync();
        }
    }

}
