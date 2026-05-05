using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.DTOs.Auth;
using PermissionManagement.API.Application.Interfaces;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _auth.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAdminRequest request)
    {
        var result = await _auth.RegisterAdminAsync(request);
        return Ok(result);
    }
}
