using PermissionManagement.API.Application.DTOs.Order;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.Services;

public class OrderService : IOrderService
{
    private readonly IPermissionEvaluator _evaluator;

    public OrderService(IPermissionEvaluator evaluator) => _evaluator = evaluator;

    private static readonly string[] Targets = ["مواطنين", "اجانب", "مزدوجو الجنسية"];

    private static readonly (int GovernorateId, string GovName)[] Govs =
    {
        (1, "Baghdad"), (2, "Basra"), (3, "Nineveh"), (4, "Erbil"),
        (5, "Najaf"), (6, "Karbala"), (7, "Anbar"), (8, "Dhi Qar"),
        (9, "Diyala"), (10, "Saladin")
    };

    public Task<IReadOnlyList<YearlyOrderDto>> GetYearlyOrdersAsync(Admin admin)
    {
        var allActions = Enum.GetValues<PermissionAction>();
        var orders = new List<YearlyOrderDto>();
        var statuses = Enum.GetValues<YearlyOrderStatus>();
        var random = new Random(1);

        for (int i = 1; i <= 20; i++)
        {
            var gov = Govs[i % Govs.Length];
            var status = statuses[i % statuses.Length];

            if (!_evaluator.CanView(admin, ServiceType.YearlyPayment, gov.GovernorateId, (int)status))
                continue;

            var allowedActions = allActions
                .Where(a => _evaluator.CanDo(admin, ServiceType.YearlyPayment, a, gov.GovernorateId))
                .Select(a => a.ToString())
                .ToList();

            orders.Add(new YearlyOrderDto(i, $"Applicant {i}", status, gov.GovernorateId, gov.GovName,
                DateTime.UtcNow.AddDays(-i), allowedActions));
        }

        return Task.FromResult<IReadOnlyList<YearlyOrderDto>>(orders);
    }

    public Task<IReadOnlyList<MonthlyOrderDto>> GetMonthlyOrdersAsync(Admin admin)
    {
        var allActions = Enum.GetValues<PermissionAction>();
        var orders = new List<MonthlyOrderDto>();
        var statuses = Enum.GetValues<MonthlyOrderStatus>();

        for (int i = 1; i <= 20; i++)
        {
            var gov = Govs[(i + 2) % Govs.Length];
            var status = statuses[i % statuses.Length];

            if (!_evaluator.CanView(admin, ServiceType.MonthlyPayment, gov.GovernorateId, (int)status))
                continue;

            var allowedActions = allActions
                .Where(a => _evaluator.CanDo(admin, ServiceType.MonthlyPayment, a, gov.GovernorateId))
                .Select(a => a.ToString())
                .ToList();

            orders.Add(new MonthlyOrderDto(i, $"Applicant {i}", status, gov.GovernorateId, gov.GovName,
                DateTime.UtcNow.AddDays(-i), allowedActions));
        }

        return Task.FromResult<IReadOnlyList<MonthlyOrderDto>>(orders);
    }

    public Task<IReadOnlyList<SafetyOrderDto>> GetSafetyOrdersAsync(Admin admin)
    {
        var allActions = Enum.GetValues<PermissionAction>();
        var orders = new List<SafetyOrderDto>();
        var statuses = Enum.GetValues<SafetyStatus>();

        for (int i = 1; i <= 20; i++)
        {
            var gov = Govs[(i + 4) % Govs.Length];
            var status = statuses[i % statuses.Length];
            var target = Targets[i % Targets.Length];

            if (!_evaluator.CanView(admin, ServiceType.SafetyCheck, gov.GovernorateId, (int)status, target))
                continue;

            var allowedActions = allActions
                .Where(a => _evaluator.CanDo(admin, ServiceType.SafetyCheck, a, gov.GovernorateId, target))
                .Select(a => a.ToString())
                .ToList();

            orders.Add(new SafetyOrderDto(i, $"Applicant {i}", status, gov.GovernorateId, gov.GovName,
                target, DateTime.UtcNow.AddDays(-i), allowedActions));
        }

        return Task.FromResult<IReadOnlyList<SafetyOrderDto>>(orders);
    }
}
