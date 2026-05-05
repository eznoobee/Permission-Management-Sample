using PermissionManagement.API.Application.DTOs.Governorate;

namespace PermissionManagement.API.Application.Interfaces;

public interface IGovernorateService
{
    Task<IReadOnlyList<GovernorateDto>> GetAllAsync();
}
