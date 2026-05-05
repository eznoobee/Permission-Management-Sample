using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Application.DTOs.Governorate;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.Application.Services;

public class GovernorateService : IGovernorateService
{
    private readonly AppDbContext _db;

    public GovernorateService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<GovernorateDto>> GetAllAsync()
    {
        return await _db.Governorates
            .OrderBy(g => g.Id)
            .Select(g => new GovernorateDto(g.Id, g.NameArabic, g.NameEnglish, g.Code))
            .ToListAsync();
    }
}
