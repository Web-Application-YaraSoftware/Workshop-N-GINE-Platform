using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IWorkshopCommandService
{
    Task<Workshop?> Handle(CreateWorkshopCommand command);
    Task<Workshop?> Handle(long workshopId, UpdateWorkshopCommand command);
}