using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Permission;

public record PermissionCheckRequest(
    int AdminId,
    ServiceType ServiceType,
    string CheckType,
    PermissionAction? Action = null,
    int? GovernorateId = null,
    int? Status = null,
    string? Target = null);
