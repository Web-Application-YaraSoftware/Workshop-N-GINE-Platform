using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Commands;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.CommandServices;

public class NotificationStateCommandService (INotificationStateRepository notificationStateRepository, IUnitOfWork unitOfWork) : INotificationStateCommandService
{
    public async Task Handle(SeedNotificationStateCommand command)
    {
        var tasks = (from ENotificationState notificationState in Enum.GetValues(typeof(ENotificationState)) select ProcessNotificationStateAsync(notificationState)).ToList();
        await Task.WhenAll(tasks);
        await unitOfWork.CompleteAsync();
    }
    
    private async Task ProcessNotificationStateAsync(ENotificationState notificationState)
    {
        try
        {
            if (!notificationStateRepository.ExistsByName(notificationState)) await notificationStateRepository.AddAsync(new NotificationState(notificationState));
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while seeding notification state '{notificationState}': {e.Message}", e);
        }
    }
}