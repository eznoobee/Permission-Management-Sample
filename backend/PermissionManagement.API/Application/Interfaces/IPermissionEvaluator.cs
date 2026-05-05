using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Application.Interfaces;

public interface IPermissionEvaluator
{
    bool CanView(Admin admin, ServiceType serviceType, int? governorateId, int? status, string? target = null);
    bool CanDo(Admin admin, ServiceType serviceType, PermissionAction action, int? governorateId, string? target = null);
}
