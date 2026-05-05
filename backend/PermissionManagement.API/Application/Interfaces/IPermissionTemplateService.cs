using PermissionManagement.API.Application.DTOs.Template;

namespace PermissionManagement.API.Application.Interfaces;

public interface IPermissionTemplateService
{
    Task<IReadOnlyList<PermissionTemplateDto>> GetAllAsync();
    Task<PermissionTemplateDto> GetByIdAsync(int id);
    Task<PermissionTemplateDto> CreateAsync(CreateTemplateRequest request);
    Task<PermissionTemplateDto> UpdateAsync(int id, CreateTemplateRequest request);
    Task DeleteAsync(int id);
}
