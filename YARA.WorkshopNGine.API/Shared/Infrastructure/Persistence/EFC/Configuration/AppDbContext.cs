using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Domain.Model.Enitites;
using YARA.WorkshopNGine.API.Device.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

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
        
        builder.Entity<Intervention>().HasKey(i => i.Id);
        builder.Entity<Intervention>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Intervention>().Property(i => i.WorkshopId).IsRequired();
        builder.Entity<Intervention>().Property(i => i.VehicleId).IsRequired();
        builder.Entity<Intervention>().Property(i => i.MechanicLeaderId).IsRequired();
        builder.Entity<Intervention>().Property(i => i.Description).IsRequired().HasMaxLength(240);
        builder.Entity<Intervention>()
            .HasMany(i => i.Tasks)
            .WithOne(t => t.Intervention)
            .HasForeignKey(t => t.InterventionId)
            .IsRequired();
        
        builder.Entity<Task>().HasKey(t => t.Id);
        builder.Entity<Task>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Task>().Property(t => t.MechanicAssignedId).IsRequired();
        builder.Entity<Task>().Property(t => t.Description).IsRequired().HasMaxLength(240);
        builder.Entity<Task>()
            .HasOne(t => t.Intervention)
            .WithMany(i => i.Tasks)
            .HasForeignKey(t => t.InterventionId)
            .IsRequired();
        builder.Entity<Task>()
            .HasMany(t => t.Checkpoints)
            .WithOne(c => c.Task)
            .HasForeignKey(c => c.TaskId)
            .IsRequired();
        
        builder.Entity<Checkpoint>().HasKey(c => c.Id);
        builder.Entity<Checkpoint>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Checkpoint>().Property(c => c.TaskId).IsRequired();
        builder.Entity<Checkpoint>().Property(c => c.Name).IsRequired().HasMaxLength(240);
        builder.Entity<Checkpoint>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Checkpoints)
            .HasForeignKey(c => c.TaskId)
            .IsRequired();
        
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(v => v.LicensePlate).IsRequired().HasMaxLength(10);
        builder.Entity<Vehicle>().Property(v => v.Brand).IsRequired().HasMaxLength(30);
        builder.Entity<Vehicle>().Property(v => v.Model).IsRequired().HasMaxLength(30);
        builder.Entity<Vehicle>().Property(v => v.UserId).IsRequired();
        
        // Inventory Context
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(240);
        builder.Entity<Product>().Property(p => p.StockQuantity).IsRequired();
        builder.Entity<Product>().Property(p => p.LowStockThreshold).IsRequired();
        builder.Entity<Product>().OwnsOne(p => p.WorkshopId, workshopId =>
        {
            workshopId.WithOwner().HasForeignKey("Id");
            workshopId.Property(w => w.Value).HasColumnName("WorkshopId").IsRequired();
        });
        
        builder.Entity<ProductRequest>().HasKey(pr => pr.Id);
        builder.Entity<ProductRequest>().Property(pr => pr.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProductRequest>().Property(pr => pr.RequestedQuantity).IsRequired();
        builder.Entity<ProductRequest>().OwnsOne(pr => pr.TaskId, taskId =>
        {
            taskId.WithOwner().HasForeignKey("Id");
            taskId.Property(t => t.Value).HasColumnName("TaskId").IsRequired();
        });
        builder.Entity<ProductRequest>().OwnsOne(pr => pr.ProductId, productId =>
        {
            productId.WithOwner().HasForeignKey("Id");
            productId.Property(p => p.Value).HasColumnName("ProductId").IsRequired();
        });
        builder.Entity<ProductRequest>().OwnsOne(pr => pr.WorkshopId, workshopId =>
        {
            workshopId.WithOwner().HasForeignKey("Id");
            workshopId.Property(w => w.Value).HasColumnName("WorkshopId").IsRequired();
        });
        builder.Entity<ProductRequest>().Property(pr => pr.Status).IsRequired();
        
      
        //Device Context
        builder.Entity<IotDevice>().HasKey(d => d.Id);
        builder.Entity<IotDevice>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<IotDevice>().Property(d => d.VehicleId).IsRequired();
        builder.Entity<IotDevice>()
            .HasMany(d => d.CodeList)
            .WithOne(c => c.IotDevice)
            .HasForeignKey(c => c.IotDeviceId)
            .IsRequired();
        
        builder.Entity<Code>().HasKey(c => c.Id);
        builder.Entity<Code>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Code>().Property(c => c.Component).IsRequired().HasMaxLength(30);
        builder.Entity<Code>().Property(c => c.ErrorCode).IsRequired().HasMaxLength(10);
        builder.Entity<Code>().Property(c => c.Description).IsRequired().HasMaxLength(150);
        builder.Entity<Code>().Property(c => c.lastUpdated).IsRequired();
        builder.Entity<Code>().Property(c => c.State).IsRequired().HasConversion(e=>e.ToString(), e=>(ECodeState)Enum.Parse(typeof(ECodeState), e));

        // Subscriptions Context
        builder.Entity<SubscriptionItem>().HasKey(s => s.Id);
        builder.Entity<SubscriptionItem>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SubscriptionItem>().Property(s => s.Status).IsRequired();
        builder.Entity<SubscriptionItem>().Property(s => s.StartedAt).IsRequired();
        builder.Entity<SubscriptionItem>().Property(s => s.EndedAt);
        builder.Entity<SubscriptionItem>().Property(s => s.CancelledAt);
        builder.Entity<SubscriptionItem>().OwnsOne(s => s.WorkshopId, workshopId =>
        {
            workshopId.WithOwner().HasForeignKey("Id");
            workshopId.Property(w => w.Value).HasColumnName("WorkshopId").IsRequired();
        });
        builder.Entity<SubscriptionItem>().OwnsOne(s => s.PlanId, planId =>
        {
            planId.WithOwner().HasForeignKey("Id");
            planId.Property(p => p.Value).HasColumnName("PlanId").IsRequired();
        });
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}