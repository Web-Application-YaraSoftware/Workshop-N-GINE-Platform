using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Commands;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.EventHandlers;

public class ApplicationReadyEventHandlerCommunication : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ApplicationReadyEventHandlerCommunication> _logger;

    public ApplicationReadyEventHandlerCommunication(IServiceProvider serviceProvider, ILogger<ApplicationReadyEventHandlerCommunication> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var notificationStateCommandService = scope.ServiceProvider.GetRequiredService<INotificationStateCommandService>();
        //var notificationCommandService = scope.ServiceProvider.GetRequiredService<INotificationCommandService>();

        await LoadNotificationStateData(notificationStateCommandService);
        //await LoadNotificationData(notificationCommandService);

    }

    public Task StopAsync(CancellationToken cancellationToken)=> Task.CompletedTask;
    
    private async Task LoadNotificationStateData(INotificationStateCommandService notificationStateCommandService)
    {
        try
        {
            var name = AppDomain.CurrentDomain.FriendlyName;
            _logger.LogInformation("Starting to seed notification states for {AppName} at {Timestamp}", name, DateTime.Now);

            var seedNotificationStateCommand = new SeedNotificationStateCommand();
            await notificationStateCommandService.Handle(seedNotificationStateCommand);

            _logger.LogInformation("Notification states seeded successfully for {AppName} at {Timestamp}", name, DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding notification states");
        }
    }
    
    /*
    private async Task LoadNotificationData(INotificationCommandService notificationCommandService)
    {
        try
        {
            var name = AppDomain.CurrentDomain.FriendlyName;
            _logger.LogInformation("Starting to seed notifications for {AppName} at {Timestamp}", name, DateTime.Now);

            var initialNotifications = new List<SeedNotificationCommand>
            {
                new ("New task assigned: Maintenance of the gearbox", 1, "activities/assistant"),
                new ("Product request approved: Neumático", 2, "inventory/requests")
                
            };
            foreach (var notification in initialNotifications)
            {
                await notificationCommandService.Handle(notification);
            }
            _logger.LogInformation("Notifications seeded successfully for {AppName} at {Timestamp}", name, DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding notifications");
        }
    }
    */

}