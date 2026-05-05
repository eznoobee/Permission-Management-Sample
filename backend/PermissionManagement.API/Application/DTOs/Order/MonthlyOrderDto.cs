using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.DTOs.Order;

public record MonthlyOrderDto(
    int Id,
    string ApplicantName,
    MonthlyOrderStatus Status,
    int GovernorateId,
    string GovernorateNameEnglish,
    DateTime CreatedAt,
    List<string> AllowedActions);
