using PermissionManagement.API.Application.DTOs.Order;
using PermissionManagement.API.Domain.Entities;

namespace PermissionManagement.API.Application.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<YearlyOrderDto>> GetYearlyOrdersAsync(Admin admin);
    Task<IReadOnlyList<MonthlyOrderDto>> GetMonthlyOrdersAsync(Admin admin);
    Task<IReadOnlyList<SafetyOrderDto>> GetSafetyOrdersAsync(Admin admin);
}
