using PermissionManagement.API.Application.DTOs.Auth;

namespace PermissionManagement.API.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RegisterAdminAsync(RegisterAdminRequest request);
}
