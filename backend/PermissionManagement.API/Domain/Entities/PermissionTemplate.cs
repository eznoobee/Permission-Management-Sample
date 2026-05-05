namespace PermissionManagement.API.Domain.Entities;

public class PermissionTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<ViewPermissionEntry> ViewPermissions { get; set; } = [];
    public List<ActionPermissionEntry> ActionPermissions { get; set; } = [];
}
