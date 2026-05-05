using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionManagement.API.Domain.Entities;

namespace PermissionManagement.API.Infrastructure.Data.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(200);
        builder.Property(a => a.PasswordHash).IsRequired();

        builder.OwnsMany(a => a.ViewPermissions, vp =>
        {
            vp.ToJson();
        });

        builder.OwnsMany(a => a.ActionPermissions, ap =>
        {
            ap.ToJson();
        });
    }
}
