using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Place here the configuration of the entities
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
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}