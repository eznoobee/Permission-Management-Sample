namespace PermissionManagement.API.Application.DTOs.Permission;

public record UpdatePermissionsRequest(
    List<ViewPermissionEntryDto> ViewPermissions,
    List<ActionPermissionEntryDto> ActionPermissions);
