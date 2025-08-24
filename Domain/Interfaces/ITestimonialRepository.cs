using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITestimonialRepository : IGenericRepository<Testimonial>
    {
        Task<List<Testimonial>> GetTopRatedAsync(int count);
    }

}
