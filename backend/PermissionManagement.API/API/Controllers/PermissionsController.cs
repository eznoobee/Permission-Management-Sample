using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.DTOs.Permission;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/permissions")]
[Authorize]
public class PermissionsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPermissionEvaluator _evaluator;

    public PermissionsController(AppDbContext db, IPermissionEvaluator evaluator)
    {
        _db = db;
        _evaluator = evaluator;
    }

    [HttpPost("check")]
    public async Task<IActionResult> Check([FromBody] PermissionCheckRequest request)
    {
        var admin = await _db.Admins.FindAsync(request.AdminId);
        if (admin is null) return NotFound(new { error = "Admin not found." });

        bool allowed = request.CheckType.ToLower() == "view"
            ? _evaluator.CanView(admin, request.ServiceType, request.GovernorateId, request.Status, request.Target)
            : request.Action.HasValue && _evaluator.CanDo(admin, request.ServiceType, request.Action.Value, request.GovernorateId, request.Target);

        return Ok(new { allowed, request.AdminId, request.ServiceType, request.CheckType });
    }
}
