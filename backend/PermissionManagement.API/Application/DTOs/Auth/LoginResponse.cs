namespace PermissionManagement.API.Application.DTOs.Auth;

public record LoginResponse(string Token, int AdminId, string Name, string Email);
