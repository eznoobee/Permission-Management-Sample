using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Order;

public record SafetyOrderDto(
    int Id,
    string ApplicantName,
    SafetyStatus Status,
    int GovernorateId,
    string GovernorateNameEnglish,
    string Target,
    DateTime CreatedAt,
    List<string> AllowedActions);
