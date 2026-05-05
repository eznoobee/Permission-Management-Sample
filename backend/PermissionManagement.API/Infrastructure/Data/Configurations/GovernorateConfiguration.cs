using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionManagement.API.Domain.Entities;

namespace PermissionManagement.API.Infrastructure.Data.Configurations;

public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.NameArabic).IsRequired().HasMaxLength(100);
        builder.Property(g => g.NameEnglish).IsRequired().HasMaxLength(100);
        builder.Property(g => g.Code).IsRequired().HasMaxLength(10);
    }
}
