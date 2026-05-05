using PermissionManagement.API.Application.DTOs.Permission;

namespace PermissionManagement.API.Application.DTOs.Template;

public record CreateTemplateRequest(
    string Name,
    string? Description,
    List<ViewPermissionEntryDto> ViewPermissions,
    List<ActionPermissionEntryDto> ActionPermissions);
