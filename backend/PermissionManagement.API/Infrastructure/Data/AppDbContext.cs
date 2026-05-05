using Microsoft.EntityFrameworkCore;
using PermissionManagement.API.Domain.Entities;

namespace PermissionManagement.API.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Governorate> Governorates => Set<Governorate>();
    public DbSet<PermissionTemplate> PermissionTemplates => Set<PermissionTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
