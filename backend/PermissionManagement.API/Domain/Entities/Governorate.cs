namespace PermissionManagement.API.Domain.Entities;

public class Governorate
{
    public int Id { get; set; }
    public string NameArabic { get; set; } = default!;
    public string NameEnglish { get; set; } = default!;
    public string Code { get; set; } = default!;
}
