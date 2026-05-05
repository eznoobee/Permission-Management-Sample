using PermissionManagement.API.Application.DTOs.Permission;

namespace PermissionManagement.API.Application.DTOs.Template;

public record PermissionTemplateDto(
    int Id,
    string Name,
    string? Description,
    bool IsActive,
    List<ViewPermissionEntryDto> ViewPermissions,
    List<ActionPermissionEntryDto> ActionPermissions);
