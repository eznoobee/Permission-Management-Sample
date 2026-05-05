using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PermissionManagement.API.Application.DTOs.Auth;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Domain.Entities;
using PermissionManagement.API.Infrastructure.Data;

namespace PermissionManagement.API.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == request.Email && a.IsActive);
        if (admin is null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return new LoginResponse(GenerateToken(admin), admin.Id, admin.Name, admin.Email);
    }

    public async Task<LoginResponse> RegisterAdminAsync(RegisterAdminRequest request)
    {
        if (await _db.Admins.AnyAsync(a => a.Email == request.Email))
            throw new InvalidOperationException("Email already registered.");

        var admin = new Admin
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _db.Admins.Add(admin);
        await _db.SaveChangesAsync();

        return new LoginResponse(GenerateToken(admin), admin.Id, admin.Name, admin.Email);
    }

    private string GenerateToken(Admin admin)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiryMinutes"]!));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, admin.Email),
            new Claim(JwtRegisteredClaimNames.Name, admin.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
