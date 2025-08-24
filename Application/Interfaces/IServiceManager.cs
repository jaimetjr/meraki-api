using Application.DTOs;

namespace Application.Interfaces
{
    public interface IServiceManager
    {
        Task<List<ServiceDto>> GetAllAsync();
        Task<ServiceDto?> GetByIdAsync(Guid id);
        Task<List<ServiceDto>> GetByCategoryAsync(string category);
        Task<ServiceDto> CreateAsync(ServiceDto dto);
        Task<bool> UpdateAsync(Guid id, ServiceDto dto);
        Task<bool> DeleteAsync(Guid id);

    }
}
