using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            return course == null ? null : _mapper.Map<CourseDto>(course);
        }

        public async Task<List<CourseDto>> GetByStatusAsync(string status)
        {
            if (!Enum.TryParse<CourseStatus>(status, out var parsedStatus))
                throw new ArgumentException($"Invalid course status: {status}");

            var courses = await _courseRepo.GetByStatusAsync(parsedStatus);
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<CourseDto> CreateAsync(CourseDto dto)
        {
            var course = new Course(
                dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                dto.Title,
                dto.Description,
                dto.Image,
                dto.Instructor,
                Enum.Parse<CourseType>(dto.Type),
                Enum.Parse<CourseStatus>(dto.Status)
            );

            if (dto.Date.HasValue && !string.IsNullOrEmpty(dto.Modality))
            {
                var modality = Enum.Parse<Modality>(dto.Modality);
                var price = new Money(dto.Price ?? 0, dto.Currency);
                course.Schedule(dto.Date.Value, modality, price, dto.Link);
            }

            await _courseRepo.AddAsync(course);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<bool> UpdateAsync(Guid id, CourseDto dto)
        {
            var existing = await _courseRepo.GetByIdAsync(id);
            if (existing == null) return false;

            var updated = new Course(
                id,
                dto.Title,
                dto.Description,
                dto.Image,
                dto.Instructor,
                Enum.Parse<CourseType>(dto.Type),
                Enum.Parse<CourseStatus>(dto.Status)
            );

            if (dto.Date.HasValue && !string.IsNullOrEmpty(dto.Modality))
            {
                var modality = Enum.Parse<Modality>(dto.Modality);
                var price = new Money(dto.Price ?? 0, dto.Currency);
                updated.Schedule(dto.Date.Value, modality, price, dto.Link);
            }

            await _courseRepo.UpdateAsync(updated);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _courseRepo.ExistsAsync(id);
            if (!exists) return false;

            await _courseRepo.DeleteAsync(id);
            return true;
        }
    }
}
