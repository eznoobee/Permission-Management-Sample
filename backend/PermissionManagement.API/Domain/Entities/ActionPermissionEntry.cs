using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Domain.Enums;

namespace PermissionManagement.API.Domain.Entities;

[Owned]
public class ActionPermissionEntry
{
    public ServiceType ServiceType { get; set; }
    public List<PermissionAction> Actions { get; set; } = [];
    public List<int>? AllowedGovernorates { get; set; }
    public List<string>? AllowedTargets { get; set; }
}
