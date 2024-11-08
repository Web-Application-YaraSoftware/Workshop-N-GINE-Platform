using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
      
        // Communication Context
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(n => n.Date).IsRequired();
        builder.Entity<Notification>().Property(n => n.Content).IsRequired().HasMaxLength(240);
        builder.Entity<Notification>().Property(n => n.UserId).IsRequired();
        builder.Entity<Notification>().Property(n => n.Endpoints).IsRequired().HasMaxLength(240);
        
        builder.Entity<NotificationState>().HasMany(s=>s.Notifications)
            .WithOne(n=>n.State)
            .HasForeignKey(n=>n.StateId)
            .HasPrincipalKey(s=>s.Id);
        
        
        builder.Entity<NotificationState>().HasKey(s => s.Id);
        builder.Entity<NotificationState>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NotificationState>().Property(s => s.Name).IsRequired().HasMaxLength(30);
        builder.Entity<NotificationState>().HasIndex(s=>s.Name).IsUnique();
        builder.Entity<NotificationState>().Property(s=>s.Name).HasConversion(e=>e.ToString(), e=>(ENotificationState)Enum.Parse(typeof(ENotificationState), e));
        
     
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
        
        
        // Profile Context
        builder.Entity<Profile>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName).HasMaxLength(100);
            entity.Property(p => p.LastName).HasMaxLength(100);
            entity.Property(p => p.Dni).IsRequired();
            entity.Property(p => p.Email).HasMaxLength(200);
            entity.Property(p => p.Age);
            entity.Property(p => p.Location).HasMaxLength(100);
            entity.Property(p => p.UserId).IsRequired();
        });
        
        // Service Context
        builder.Entity<Workshop>().HasKey(w => w.Id);
        builder.Entity<Workshop>().Property(w => w.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Workshop>().Property(w => w.Name).IsRequired().HasMaxLength(100);
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}