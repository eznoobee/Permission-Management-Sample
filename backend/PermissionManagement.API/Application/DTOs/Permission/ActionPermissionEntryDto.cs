using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Permission;

public record ActionPermissionEntryDto(
    ServiceType ServiceType,
    List<PermissionAction> Actions,
    List<int>? AllowedGovernorates,
    List<string>? AllowedTargets);
