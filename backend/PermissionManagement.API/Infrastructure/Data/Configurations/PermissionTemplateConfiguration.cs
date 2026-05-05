using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionManagement.API.Domain.Entities;

namespace PermissionManagement.API.Infrastructure.Data.Configurations;

public class PermissionTemplateConfiguration : IEntityTypeConfiguration<PermissionTemplate>
{
    public void Configure(EntityTypeBuilder<PermissionTemplate> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(200);

        builder.OwnsMany(t => t.ViewPermissions, vp =>
        {
            vp.ToJson();
        });

        builder.OwnsMany(t => t.ActionPermissions, ap =>
        {
            ap.ToJson();
        });
    }
}
