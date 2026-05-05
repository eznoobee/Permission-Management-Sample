namespace PermissionManagement.API.Application.DTOs.Auth;

public record RegisterAdminRequest(string Name, string Email, string Password);
