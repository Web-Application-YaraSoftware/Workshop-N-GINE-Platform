using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IInterventionCommandService
{
    Task<Intervention?> Handle(CreateInterventionCommand command);
    
    Task<Intervention?> Handle(long interventionId, UpdateInterventionCommand command);
}