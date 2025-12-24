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
        private readonly ICategoryRepository _categoryRepo;
        private readonly IBenefitRepository _benefitRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceManager(IServiceRepository serviceRepo, ICategoryRepository categoryRepo, IBenefitRepository benefitRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _serviceRepo = serviceRepo;
            _categoryRepo = categoryRepo;
            _benefitRepo = benefitRepo;
            _unitOfWork = unitOfWork;
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
            
            // Create Money value object from DTO
            var price = new Money(dto.Price, dto.Currency);
            
            // Create Service entity using constructor
            var service = new Service(
                dto.Name,
                dto.Description,
                dto.Image,
                price
            );
            
            // Handle optional properties
            if (!string.IsNullOrWhiteSpace(dto.LongDescription) || !string.IsNullOrWhiteSpace(dto.Duration))
            {
                service.UpdateDetails(dto.LongDescription, dto.Duration);
            }
            
            // Handle Category relationship
            if (dto.Category != null)
            {
                Category? category = null;
                if (dto.Category.Id != Guid.Empty)
                {
                    // Load existing category
                    category = await _categoryRepo.GetByIdAsync(dto.Category.Id);
                }
                
                if (category == null)
                {
                    // Create new category and add to repository for proper tracking
                    category = new Category(dto.Category.Name);
                    await _categoryRepo.AddAsync(category);
                }
                
                service.UpdateCategory(category);
            }
            
            // Handle Benefits relationship
            if (dto.Benefits != null)
            {
                foreach (var benefitDto in dto.Benefits)
                {
                    Benefit? benefit = null;
                    if (benefitDto.Id != Guid.Empty)
                    {
                        // Load existing benefit
                        benefit = await _benefitRepo.GetByIdAsync(benefitDto.Id);
                    }
                    
                    if (benefit == null)
                    {
                        // Create new benefit and add to repository for proper tracking
                        benefit = new Benefit(benefitDto.Title, benefitDto.Description);
                        await _benefitRepo.AddAsync(benefit);
                    }
                    
                    service.AddBenefit(benefit);
                }
            }
            
            await _serviceRepo.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();
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
                Category? category = null;
                if (dto.Category.Id != Guid.Empty)
                {
                    // Load existing category
                    category = await _categoryRepo.GetByIdAsync(dto.Category.Id);
                }
                
                if (category == null)
                {
                    // Create new category and add to repository for proper tracking
                    category = new Category(dto.Category.Name);
                    await _categoryRepo.AddAsync(category);
                }
                
                existing.UpdateCategory(category);
            }

            if (dto.Benefits != null)
            {
                foreach (var benefitDto in dto.Benefits)
                {
                    Benefit? benefit = null;
                    if (benefitDto.Id != Guid.Empty)
                    {
                        // Load existing benefit
                        benefit = await _benefitRepo.GetByIdAsync(benefitDto.Id);
                    }
                    
                    if (benefit == null)
                    {
                        // Create new benefit and add to repository for proper tracking
                        benefit = new Benefit(benefitDto.Title, benefitDto.Description);
                        await _benefitRepo.AddAsync(benefit);
                    }
                    
                    existing.AddBenefit(benefit);
                }
            }

            await _serviceRepo.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _serviceRepo.ExistsAsync(id);
            if (!exists) return false;

            await _serviceRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
