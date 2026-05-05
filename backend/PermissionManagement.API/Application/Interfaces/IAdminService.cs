using PermissionManagement.API.Application.DTOs.Permission;

namespace PermissionManagement.API.Application.Interfaces;

public interface IAdminService
{
    Task<IReadOnlyList<AdminPermissionsDto>> GetAllAsync();
    Task<AdminPermissionsDto> GetByIdAsync(int id);
    Task<AdminPermissionsDto> GetPermissionsAsync(int id);
    Task<AdminPermissionsDto> UpdatePermissionsAsync(int id, UpdatePermissionsRequest request);
    Task<AdminPermissionsDto> ApplyTemplateAsync(int adminId, int templateId);
}
