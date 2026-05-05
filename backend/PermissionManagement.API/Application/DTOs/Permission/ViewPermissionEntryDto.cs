using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Permission;

public record ViewPermissionEntryDto(
    ServiceType ServiceType,
    List<int>? AllowedStatuses,
    List<int>? AllowedGovernorates,
    List<string>? AllowedTargets);
