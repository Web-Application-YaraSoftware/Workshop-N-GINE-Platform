using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.CommandServices;
using YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.EventHandlers;
using YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.QueryServices;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;
using YARA.WorkshopNGine.API.CommunicationManagement.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Profiles.Application.Internal.CommandServices;
using YARA.WorkshopNGine.API.Profiles.Application.Internal.QueryServices;
using YARA.WorkshopNGine.API.Profiles.Domain.Repositories;
using YARA.WorkshopNGine.API.Profiles.Domain.Services;
using YARA.WorkshopNGine.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Profiles.interfaces.ACL;
using YARA.WorkshopNGine.API.Profiles.interfaces.ACL.Services;
using YARA.WorkshopNGine.API.IAM.Application.Internal.CommandServices;
using YARA.WorkshopNGine.API.IAM.Application.Internal.EventHandlers;
using YARA.WorkshopNGine.API.IAM.Application.Internal.QueryServices;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.IAM.Domain.Services;
using YARA.WorkshopNGine.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.IAM.Interfaces.ACL;
using YARA.WorkshopNGine.API.IAM.Interfaces.ACL.Services;
using YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;
using YARA.WorkshopNGine.API.Service.Application.Internal.OutboundServices.ACL;
using YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Interfaces.APS.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Configuration for Routing
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString == null) return;
    if (builder.Environment.IsDevelopment()) 
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction()) 
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => { c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YARA.WorkshopNGine.API",
        Version = "v1",
        Description = "YARA WorkshopNGine Platform API",
        TermsOfService = new Uri("https://yara-workshop.com/tos"),
        Contact = new OpenApiContact {Name = "YARA WorkshopNGine", Email = "contact@yara.com"},
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    }); });

// Configure Dependency Injection

// Profiles Bounded Context Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Communication Management Bounded Context Injection Configuration
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();
builder.Services.AddScoped<INotificationStateCommandService, NotificationStateCommandService>();
builder.Services.AddScoped<INotificationStateRepository, NotificationStateRepository>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Service Bounded Context Injection Configuration
builder.Services.AddScoped<ExternalIamService>();
builder.Services.AddScoped<ExternalProfileService>();
builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
builder.Services.AddScoped<IWorkshopCommandService, WorkshopCommandService>();
builder.Services.AddScoped<IWorkshopQueryService, WorkshopQueryService>();
builder.Services.AddScoped<IInterventionRepository, InterventionRepository>();
builder.Services.AddScoped<IInterventionCommandService, InterventionCommandService>();
builder.Services.AddScoped<IInterventionQueryService, InterventionQueryService>();

// Event Handlers
builder.Services.AddHostedService<ApplicationReadyEventHandler>();
builder.Services.AddHostedService<ApplicationReadyEventHandlerCommunication>();

var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();