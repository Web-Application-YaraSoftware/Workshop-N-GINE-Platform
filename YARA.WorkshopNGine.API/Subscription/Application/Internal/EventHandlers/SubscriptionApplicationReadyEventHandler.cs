using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.EventHandlers;

public class SubscriptionApplicationReadyEventHandler : IHostedService
{
    private readonly ILogger<SubscriptionApplicationReadyEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    
    public SubscriptionApplicationReadyEventHandler(
        ILogger<SubscriptionApplicationReadyEventHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var subscriptionCommandService = scope.ServiceProvider.GetRequiredService<IPlanCommandService>();
        
        await LoadPlansData(subscriptionCommandService);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    private async Task LoadPlansData(IPlanCommandService subscriptionCommandService)
    {
        try
        {
            var name = AppDomain.CurrentDomain.FriendlyName;
            _logger.LogInformation("Starting to seed plans for {AppName} at {Timestamp}", name, DateTime.Now);
            
            var seedPlansCommand = new SeedPlansCommand();
            await subscriptionCommandService.Handle(seedPlansCommand);
            
            _logger.LogInformation("Plans seeded successfully for {AppName} at {Timestamp}", name, DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding plans");
        }
    }
}