using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestimonialService(ITestimonialRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TestimonialDto>> GetAllAsync() =>
            _mapper.Map<List<TestimonialDto>>(await _repo.GetAllAsync());

        public async Task<TestimonialDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TestimonialDto>(entity);
        }

        public async Task<List<TestimonialDto>> GetTopRatedAsync(int count)
        {
            var top = await _repo.GetTopRatedAsync(count);
            return _mapper.Map<List<TestimonialDto>>(top);
        }

        public async Task<TestimonialDto> CreateAsync(TestimonialDto dto)
        {
            // Create Testimonial entity using constructor
            var testimonial = new Testimonial(
                dto.AuthorName,
                dto.AuthorAvatarUrl,
                dto.Rating,
                dto.Content,
                dto.AuthorBadge
            );
            
            await _repo.AddAsync(testimonial);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TestimonialDto>(testimonial);
        }

        public async Task<bool> UpdateAsync(Guid id, TestimonialDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Update(dto.AuthorName, dto.AuthorAvatarUrl, dto.Rating, dto.Content, dto.AuthorBadge);
            await _repo.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists) return false;

            await _repo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
