using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _therapistRepo;
        private readonly IMapper _mapper;

        public TherapistService(ITherapistRepository therapistRepo, IMapper mapper)
        {
            _therapistRepo = therapistRepo;
            _mapper = mapper;
        }

        public async Task<List<TherapistDto>> GetAllAsync()
        {
            var therapists = await _therapistRepo.GetAllAsync();
            return _mapper.Map<List<TherapistDto>>(therapists);
        }

        public async Task<TherapistDto?> GetByIdAsync(Guid id)
        {
            var therapist = await _therapistRepo.GetByIdAsync(id);
            return therapist == null ? null : _mapper.Map<TherapistDto>(therapist);
        }

        public async Task<List<TherapistDto>> FindBySpecialtyAsync(string specialtyName)
        {
            var therapists = await _therapistRepo.FindBySpecialtyAsync(specialtyName);
            return _mapper.Map<List<TherapistDto>>(therapists);
        }

        public async Task<TherapistDto> CreateAsync(TherapistDto dto)
        {
            var therapist = _mapper.Map<Therapist>(dto);
            await _therapistRepo.AddAsync(therapist);
            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task<bool> UpdateAsync(Guid id, TherapistDto dto)
        {
            var existing = await _therapistRepo.GetByIdAsync(id);
            if (existing == null) return false;

            var updated = new Therapist(
                id,
                dto.Name,
                dto.Bio,
                dto.Image,
                dto.Experience,
                dto.Education
            );

            if (dto.Specialties != null)
            {
                foreach (var specialtyDto in dto.Specialties)
                {
                    var specialty = new Specialty(
                        specialtyDto.Id != Guid.Empty ? specialtyDto.Id : Guid.NewGuid(),
                        specialtyDto.Name,
                        specialtyDto.Description
                    );
                    updated.AddSpecialty(specialty);
                }
            }

            await _therapistRepo.UpdateAsync(updated);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _therapistRepo.ExistsAsync(id);
            if (!exists) return false;

            await _therapistRepo.DeleteAsync(id);
            return true;
        }

    }
}
