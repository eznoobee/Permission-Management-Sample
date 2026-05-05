using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOrderService _orders;

    public OrdersController(AppDbContext db, IOrderService orders)
    {
        _db = db;
        _orders = orders;
    }

    [HttpGet("yearly")]
    public async Task<IActionResult> GetYearly()
    {
        var admin = await GetCurrentAdmin();
        if (admin is null) return Unauthorized();
        return Ok(await _orders.GetYearlyOrdersAsync(admin));
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthly()
    {
        var admin = await GetCurrentAdmin();
        if (admin is null) return Unauthorized();
        return Ok(await _orders.GetMonthlyOrdersAsync(admin));
    }

    [HttpGet("safety")]
    public async Task<IActionResult> GetSafety()
    {
        var admin = await GetCurrentAdmin();
        if (admin is null) return Unauthorized();
        return Ok(await _orders.GetSafetyOrdersAsync(admin));
    }

    private async Task<Domain.Entities.Admin?> GetCurrentAdmin()
    {
        var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? User.FindFirstValue("sub");
        if (!int.TryParse(idStr, out var id)) return null;
        return await _db.Admins.FindAsync(id);
    }
}
