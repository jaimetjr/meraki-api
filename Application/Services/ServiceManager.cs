using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;

        public ServiceManager(IServiceRepository serviceRepo, IMapper mapper)
        {
            _serviceRepo = serviceRepo;
            _mapper = mapper;
        }

        public async Task<List<ServiceDto>> GetAllAsync()
        {
            var services = await _serviceRepo.GetAllAsync();
            return _mapper.Map<List<ServiceDto>>(services);
        }

        public async Task<ServiceDto?> GetByIdAsync(Guid id)
        {
            var service = await _serviceRepo.GetByIdAsync(id);
            return service == null ? null : _mapper.Map<ServiceDto>(service);
        }

        public async Task<List<ServiceDto>> GetByCategoryAsync(string category)
        {
            var services = await _serviceRepo.GetByCategoryAsync(category);
            return _mapper.Map<List<ServiceDto>>(services);
        }

        public async Task<ServiceDto> CreateAsync(ServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            await _serviceRepo.AddAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<bool> UpdateAsync(Guid id, ServiceDto dto)
        {
            var existing = await _serviceRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Update(dto.Name, dto.Description, dto.Image, new Money(dto.Price, dto.Currency));
            existing.UpdateDetails(dto.LongDescription, dto.Duration);

            if (dto.Category != null)
            {
                var category = new Category(dto.Category.Id, dto.Category.Name);
                existing.UpdateCategory(category);
            }

            if (dto.Benefits != null)
            {
                foreach (var benefitDto in dto.Benefits)
                {
                    var benefit = new Benefit(
                        benefitDto.Id != Guid.Empty ? benefitDto.Id : Guid.NewGuid(),
                        benefitDto.Title,
                        benefitDto.Description
                    );
                    existing.AddBenefit(benefit);
                }
            }

            await _serviceRepo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _serviceRepo.ExistsAsync(id);
            if (!exists) return false;

            await _serviceRepo.DeleteAsync(id);
            return true;
        }
    }
}
