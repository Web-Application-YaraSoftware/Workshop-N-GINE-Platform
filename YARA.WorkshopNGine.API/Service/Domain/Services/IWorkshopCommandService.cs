using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IWorkshopCommandService
{
    Task<Workshop?> Handle(CreateWorkshopCommand command);
    Task<Workshop?> Handle(long workshopId, UpdateWorkshopCommand command);
    
    Task<UserId?> Handle(long workshopId, CreateClientCommand command);
    
    Task<UserId?> Handle(long workshopId, CreateMechanicCommand command);
}