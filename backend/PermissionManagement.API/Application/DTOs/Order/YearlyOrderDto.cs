using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Order;

public record YearlyOrderDto(
    int Id,
    string ApplicantName,
    YearlyOrderStatus Status,
    int GovernorateId,
    string GovernorateNameEnglish,
    DateTime CreatedAt,
    List<string> AllowedActions);
