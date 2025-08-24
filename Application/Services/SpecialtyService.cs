using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepo;
        private readonly IMapper _mapper;

        public SpecialtyService(ISpecialtyRepository specialtyRepo, IMapper mapper)
        {
            _specialtyRepo = specialtyRepo;
            _mapper = mapper;
        }

        public async Task<List<SpecialtyDto>> GetAllAsync()
        {
            var specialties = await _specialtyRepo.GetAllAsync();
            return _mapper.Map<List<SpecialtyDto>>(specialties);
        }

        public async Task<SpecialtyDto?> GetByIdAsync(Guid id)
        {
            var specialty = await _specialtyRepo.GetByIdAsync(id);
            return specialty == null ? null : _mapper.Map<SpecialtyDto>(specialty);
        }

        public async Task<SpecialtyDto?> GetByNameAsync(string name)
        {
            var specialty = await _specialtyRepo.GetByNameAsync(name);
            return specialty == null ? null : _mapper.Map<SpecialtyDto>(specialty);
        }

        public async Task<SpecialtyDto> CreateAsync(SpecialtyDto dto)
        {
            var specialty = new Specialty(dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(), dto.Name, dto.Description);
            await _specialtyRepo.AddAsync(specialty);
            return _mapper.Map<SpecialtyDto>(specialty);
        }

        public async Task<bool> UpdateAsync(Guid id, SpecialtyDto dto)
        {
            var existing = await _specialtyRepo.GetByIdAsync(id);
            if (existing == null) return false;

            var updated = new Specialty(id, dto.Name, dto.Description);
            await _specialtyRepo.UpdateAsync(updated);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _specialtyRepo.ExistsAsync(id);
            if (!exists) return false;

            await _specialtyRepo.DeleteAsync(id);
            return true;
        }

    }
}
