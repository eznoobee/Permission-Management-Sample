namespace PermissionManagement.API.Domain.Entities;

public class Admin
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<ViewPermissionEntry> ViewPermissions { get; set; } = [];
    public List<ActionPermissionEntry> ActionPermissions { get; set; } = [];
}
