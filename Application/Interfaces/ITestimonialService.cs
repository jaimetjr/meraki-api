using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITestimonialService
    {
        Task<List<TestimonialDto>> GetAllAsync();
        Task<TestimonialDto?> GetByIdAsync(Guid id);
        Task<List<TestimonialDto>> GetTopRatedAsync(int count);
        Task<TestimonialDto> CreateAsync(TestimonialDto dto);
        Task<bool> UpdateAsync(Guid id, TestimonialDto dto);
        Task<bool> DeleteAsync(Guid id);
    }

}
