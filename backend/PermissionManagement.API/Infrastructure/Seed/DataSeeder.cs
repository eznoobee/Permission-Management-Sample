using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Domain.Enums;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Governorates.AnyAsync())
            await SeedGovernoratesAsync(db);

        if (!await db.PermissionTemplates.AnyAsync())
            await SeedTemplatesAsync(db);

        if (!await db.Admins.AnyAsync())
            await SeedAdminsAsync(db);
    }

    private static async Task SeedGovernoratesAsync(AppDbContext db)
    {
        var governorates = new List<Governorate>
        {
            new() { Id = 1,  NameArabic = "بغداد",      NameEnglish = "Baghdad",       Code = "BGH" },
            new() { Id = 2,  NameArabic = "البصرة",     NameEnglish = "Basra",         Code = "BSR" },
            new() { Id = 3,  NameArabic = "نينوى",      NameEnglish = "Nineveh",       Code = "NNW" },
            new() { Id = 4,  NameArabic = "أربيل",      NameEnglish = "Erbil",         Code = "ERB" },
            new() { Id = 5,  NameArabic = "النجف",      NameEnglish = "Najaf",         Code = "NJF" },
            new() { Id = 6,  NameArabic = "كربلاء",     NameEnglish = "Karbala",       Code = "KRB" },
            new() { Id = 7,  NameArabic = "الأنبار",    NameEnglish = "Anbar",         Code = "ANB" },
            new() { Id = 8,  NameArabic = "ذي قار",     NameEnglish = "Dhi Qar",       Code = "DHQ" },
            new() { Id = 9,  NameArabic = "ديالى",      NameEnglish = "Diyala",        Code = "DYL" },
            new() { Id = 10, NameArabic = "صلاح الدين", NameEnglish = "Saladin",       Code = "SLD" },
            new() { Id = 11, NameArabic = "كركوك",      NameEnglish = "Kirkuk",        Code = "KRK" },
            new() { Id = 12, NameArabic = "واسط",       NameEnglish = "Wasit",         Code = "WST" },
            new() { Id = 13, NameArabic = "بابل",       NameEnglish = "Babylon",       Code = "BBL" },
            new() { Id = 14, NameArabic = "ميسان",      NameEnglish = "Maysan",        Code = "MSN" },
            new() { Id = 15, NameArabic = "المثنى",     NameEnglish = "Muthanna",      Code = "MTN" },
            new() { Id = 16, NameArabic = "القادسية",   NameEnglish = "Al-Qadisiyah", Code = "QDS" },
            new() { Id = 17, NameArabic = "السليمانية", NameEnglish = "Sulaymaniyah",  Code = "SLY" },
            new() { Id = 18, NameArabic = "دهوك",       NameEnglish = "Duhok",         Code = "DHK" },
        };
        db.Governorates.AddRange(governorates);
        await db.SaveChangesAsync();
    }

    private static async Task SeedTemplatesAsync(AppDbContext db)
    {
        var nationalViewer = new PermissionTemplate
        {
            Name = "National Viewer",
            Description = "Read-only access to all order types in all governorates",
            ViewPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment, AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.MonthlyPayment, AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.SafetyCheck, AllowedStatuses = null, AllowedGovernorates = null }
            ],
            ActionPermissions = []
        };

        var baghdadApprover = new PermissionTemplate
        {
            Name = "Baghdad Approver",
            Description = "Approve and process yearly/monthly orders in Baghdad",
            ViewPermissions = [],
            ActionPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment,  Actions = [PermissionAction.Approve, PermissionAction.Reject, PermissionAction.ChangeStatus], AllowedGovernorates = [1] },
                new() { ServiceType = ServiceType.MonthlyPayment, Actions = [PermissionAction.Approve, PermissionAction.ChangeStatus], AllowedGovernorates = [1] }
            ]
        };

        var safetyProcessorSouth = new PermissionTemplate
        {
            Name = "Safety Processor (South)",
            Description = "View and process safety check orders in Basra and Nineveh",
            ViewPermissions =
            [
                new() { ServiceType = ServiceType.SafetyCheck, AllowedStatuses = [(int)SafetyStatus.OnReview, (int)SafetyStatus.PaidConfirmed, (int)SafetyStatus.Completed], AllowedGovernorates = [2, 3] }
            ],
            ActionPermissions =
            [
                new() { ServiceType = ServiceType.SafetyCheck, Actions = [PermissionAction.Approve, PermissionAction.Reject], AllowedGovernorates = [2, 3] }
            ]
        };

        db.PermissionTemplates.AddRange(nationalViewer, baghdadApprover, safetyProcessorSouth);
        await db.SaveChangesAsync();
    }

    private static async Task SeedAdminsAsync(AppDbContext db)
    {
        var superAdmin = new Admin
        {
            Name = "Super Admin",
            Email = "super@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            ViewPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment,  AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.MonthlyPayment, AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.SafetyCheck,    AllowedStatuses = null, AllowedGovernorates = null }
            ],
            ActionPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment,  Actions = [PermissionAction.Approve, PermissionAction.Reject, PermissionAction.ChangeStatus], AllowedGovernorates = [1] },
                new() { ServiceType = ServiceType.MonthlyPayment, Actions = [PermissionAction.Approve, PermissionAction.ChangeStatus], AllowedGovernorates = [1] },
                new() { ServiceType = ServiceType.YearlyPayment,  Actions = [PermissionAction.Export, PermissionAction.Print], AllowedGovernorates = null }
            ]
        };

        var basraReviewer = new Admin
        {
            Name = "Basra Reviewer",
            Email = "basra@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            ViewPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment, AllowedStatuses = [(int)YearlyOrderStatus.Reviewing, (int)YearlyOrderStatus.FinalReview], AllowedGovernorates = [2] },
                new() { ServiceType = ServiceType.SafetyCheck,   AllowedStatuses = [(int)SafetyStatus.OnReview, (int)SafetyStatus.PaidConfirmed], AllowedGovernorates = null }
            ],
            ActionPermissions =
            [
                new() { ServiceType = ServiceType.SafetyCheck, Actions = [PermissionAction.Reject], AllowedGovernorates = [2, 3] }
            ]
        };

        var readOnly = new Admin
        {
            Name = "Read Only",
            Email = "readonly@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            ViewPermissions =
            [
                new() { ServiceType = ServiceType.YearlyPayment,  AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.MonthlyPayment, AllowedStatuses = null, AllowedGovernorates = null },
                new() { ServiceType = ServiceType.SafetyCheck,    AllowedStatuses = null, AllowedGovernorates = null }
            ],
            ActionPermissions = []
        };

        db.Admins.AddRange(superAdmin, basraReviewer, readOnly);
        await db.SaveChangesAsync();
    }
}
