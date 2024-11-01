using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;

public interface INotificationStateCommandService
{
    Task Handle(SeedNotificationStateCommand command);
}