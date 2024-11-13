using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Domain.Services;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.EventHandlers;

public class ApplicationReadyEventHandler : IHostedService
{
    private readonly ILogger<ApplicationReadyEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationReadyEventHandler(
        ILogger<ApplicationReadyEventHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var roleCommandService = scope.ServiceProvider.GetRequiredService<IRoleCommandService>();

        await LoadRolesData(roleCommandService);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task LoadRolesData(IRoleCommandService roleCommandService)
    {
        try
        {
            var name = AppDomain.CurrentDomain.FriendlyName;
            _logger.LogInformation("Starting to seed roles for {AppName} at {Timestamp}", name, DateTime.Now);

            var seedRolesCommand = new SeedRolesCommand();
            await roleCommandService.Handle(seedRolesCommand);

            _logger.LogInformation("Roles seeded successfully for {AppName} at {Timestamp}", name, DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding roles");
        }
    }
}