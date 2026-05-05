using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Application.DTOs.Permission;
using PermissionManagement.API.Application.DTOs.Template;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.Application.Services;

public class PermissionTemplateService : IPermissionTemplateService
{
    private readonly AppDbContext _db;

    public PermissionTemplateService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<PermissionTemplateDto>> GetAllAsync()
    {
        var templates = await _db.PermissionTemplates.ToListAsync();
        return templates.Select(ToDto).ToList();
    }

    public async Task<PermissionTemplateDto> GetByIdAsync(int id)
    {
        var template = await _db.PermissionTemplates.FindAsync(id)
            ?? throw new KeyNotFoundException($"Template {id} not found.");
        return ToDto(template);
    }

    public async Task<PermissionTemplateDto> CreateAsync(CreateTemplateRequest request)
    {
        var template = new PermissionTemplate
        {
            Name = request.Name,
            Description = request.Description,
            ViewPermissions = request.ViewPermissions.Select(v => new ViewPermissionEntry
            {
                ServiceType = v.ServiceType,
                AllowedStatuses = v.AllowedStatuses,
                AllowedGovernorates = v.AllowedGovernorates,
                AllowedTargets = v.AllowedTargets
            }).ToList(),
            ActionPermissions = request.ActionPermissions.Select(a => new ActionPermissionEntry
            {
                ServiceType = a.ServiceType,
                Actions = a.Actions,
                AllowedGovernorates = a.AllowedGovernorates,
                AllowedTargets = a.AllowedTargets
            }).ToList()
        };

        _db.PermissionTemplates.Add(template);
        await _db.SaveChangesAsync();
        return ToDto(template);
    }

    public async Task<PermissionTemplateDto> UpdateAsync(int id, CreateTemplateRequest request)
    {
        var template = await _db.PermissionTemplates.FindAsync(id)
            ?? throw new KeyNotFoundException($"Template {id} not found.");

        template.Name = request.Name;
        template.Description = request.Description;
        template.ViewPermissions = request.ViewPermissions.Select(v => new ViewPermissionEntry
        {
            ServiceType = v.ServiceType,
            AllowedStatuses = v.AllowedStatuses,
            AllowedGovernorates = v.AllowedGovernorates,
            AllowedTargets = v.AllowedTargets
        }).ToList();
        template.ActionPermissions = request.ActionPermissions.Select(a => new ActionPermissionEntry
        {
            ServiceType = a.ServiceType,
            Actions = a.Actions,
            AllowedGovernorates = a.AllowedGovernorates,
            AllowedTargets = a.AllowedTargets
        }).ToList();

        await _db.SaveChangesAsync();
        return ToDto(template);
    }

    public async Task DeleteAsync(int id)
    {
        var template = await _db.PermissionTemplates.FindAsync(id)
            ?? throw new KeyNotFoundException($"Template {id} not found.");
        _db.PermissionTemplates.Remove(template);
        await _db.SaveChangesAsync();
    }

    private static PermissionTemplateDto ToDto(PermissionTemplate t) => new(
        t.Id, t.Name, t.Description, t.IsActive,
        t.ViewPermissions.Select(v => new ViewPermissionEntryDto(v.ServiceType, v.AllowedStatuses, v.AllowedGovernorates, v.AllowedTargets)).ToList(),
        t.ActionPermissions.Select(a => new ActionPermissionEntryDto(a.ServiceType, a.Actions, a.AllowedGovernorates, a.AllowedTargets)).ToList());
}
