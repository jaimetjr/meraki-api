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
        private readonly IMapper _mapper;

        public TestimonialService(ITestimonialRepository repo, IMapper mapper)
        {
            _repo = repo;
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
            var entity = _mapper.Map<Testimonial>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<TestimonialDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, TestimonialDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Update(dto.AuthorName, dto.AuthorAvatarUrl, dto.Rating, dto.Content, dto.AuthorBadge);
            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }

}
