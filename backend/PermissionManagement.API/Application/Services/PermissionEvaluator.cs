using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.Services;

public class PermissionEvaluator : IPermissionEvaluator
{
    public bool CanView(Admin admin, ServiceType serviceType, int? governorateId, int? status, string? target = null)
    {
        return admin.ViewPermissions.Any(p =>
            p.ServiceType == serviceType &&
            (p.AllowedStatuses is null || status is null || p.AllowedStatuses.Contains(status.Value)) &&
            (p.AllowedGovernorates is null || governorateId is null || p.AllowedGovernorates.Contains(governorateId.Value)) &&
            (p.AllowedTargets is null || target is null || p.AllowedTargets.Contains(target)));
    }

    public bool CanDo(Admin admin, ServiceType serviceType, PermissionAction action, int? governorateId, string? target = null)
    {
        return admin.ActionPermissions.Any(p =>
            p.ServiceType == serviceType &&
            p.Actions.Contains(action) &&
            (p.AllowedGovernorates is null || governorateId is null || p.AllowedGovernorates.Contains(governorateId.Value)) &&
            (p.AllowedTargets is null || target is null || p.AllowedTargets.Contains(target)));
    }
}
