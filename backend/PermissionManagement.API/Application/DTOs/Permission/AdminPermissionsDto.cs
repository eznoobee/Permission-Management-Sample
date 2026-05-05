namespace PermissionManagement.API.Application.DTOs.Permission;

public record AdminPermissionsDto(
    int AdminId,
    string Name,
    string Email,
    bool IsActive,
    List<ViewPermissionEntryDto> ViewPermissions,
    List<ActionPermissionEntryDto> ActionPermissions);
