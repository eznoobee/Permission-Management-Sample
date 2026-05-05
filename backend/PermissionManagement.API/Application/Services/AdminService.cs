using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Application.DTOs.Permission;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.Application.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _db;

    public AdminService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<AdminPermissionsDto>> GetAllAsync()
    {
        var admins = await _db.Admins.ToListAsync();
        return admins.Select(ToDto).ToList();
    }

    public async Task<AdminPermissionsDto> GetByIdAsync(int id)
    {
        var admin = await _db.Admins.FindAsync(id)
            ?? throw new KeyNotFoundException($"Admin {id} not found.");
        return ToDto(admin);
    }

    public async Task<AdminPermissionsDto> GetPermissionsAsync(int id) => await GetByIdAsync(id);

    public async Task<AdminPermissionsDto> UpdatePermissionsAsync(int id, UpdatePermissionsRequest request)
    {
        var admin = await _db.Admins.FindAsync(id)
            ?? throw new KeyNotFoundException($"Admin {id} not found.");

        admin.ViewPermissions = request.ViewPermissions.Select(v => new ViewPermissionEntry
        {
            ServiceType = v.ServiceType,
            AllowedStatuses = v.AllowedStatuses,
            AllowedGovernorates = v.AllowedGovernorates,
            AllowedTargets = v.AllowedTargets
        }).ToList();

        admin.ActionPermissions = request.ActionPermissions.Select(a => new ActionPermissionEntry
        {
            ServiceType = a.ServiceType,
            Actions = a.Actions,
            AllowedGovernorates = a.AllowedGovernorates,
            AllowedTargets = a.AllowedTargets
        }).ToList();

        await _db.SaveChangesAsync();
        return ToDto(admin);
    }

    public async Task<AdminPermissionsDto> ApplyTemplateAsync(int adminId, int templateId)
    {
        var admin = await _db.Admins.FindAsync(adminId)
            ?? throw new KeyNotFoundException($"Admin {adminId} not found.");
        var template = await _db.PermissionTemplates.FindAsync(templateId)
            ?? throw new KeyNotFoundException($"Template {templateId} not found.");

        admin.ViewPermissions = template.ViewPermissions.Select(vp => new ViewPermissionEntry
        {
            ServiceType = vp.ServiceType,
            AllowedStatuses = vp.AllowedStatuses?.ToList(),
            AllowedGovernorates = vp.AllowedGovernorates?.ToList(),
            AllowedTargets = vp.AllowedTargets?.ToList()
        }).ToList();

        admin.ActionPermissions = template.ActionPermissions.Select(ap => new ActionPermissionEntry
        {
            ServiceType = ap.ServiceType,
            Actions = ap.Actions.ToList(),
            AllowedGovernorates = ap.AllowedGovernorates?.ToList(),
            AllowedTargets = ap.AllowedTargets?.ToList()
        }).ToList();

        await _db.SaveChangesAsync();
        return ToDto(admin);
    }

    private static AdminPermissionsDto ToDto(Admin a) => new(
        a.Id, a.Name, a.Email, a.IsActive,
        a.ViewPermissions.Select(v => new ViewPermissionEntryDto(v.ServiceType, v.AllowedStatuses, v.AllowedGovernorates, v.AllowedTargets)).ToList(),
        a.ActionPermissions.Select(ap => new ActionPermissionEntryDto(ap.ServiceType, ap.Actions, ap.AllowedGovernorates, ap.AllowedTargets)).ToList());
}
