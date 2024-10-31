using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // IAM Context
        
        builder.Entity<Role>().HasKey(u => u.Id);
        builder.Entity<Role>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Role>().Property(u => u.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Role>().HasIndex(u => u.Name).IsUnique();
        builder.Entity<Role>().Property(u => u.Name).HasConversion(v => v.ToString(), v => (Roles)Enum.Parse(typeof(Roles), v));
        
        builder.Entity<Role>().HasMany(c => c.Users)
            .WithOne(t => t.Role)
            .HasForeignKey(t => t.RoleId)
            .HasPrincipalKey(c => c.Id);
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        builder.Entity<User>().Property(u => u.RoleId).IsRequired();
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}