using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateInterventionCommandFromResourceAssembler
{
    public static CreateInterventionCommand ToCommandFromResource(CreateInterventionResource resource)
    {
        return new CreateInterventionCommand(resource.WorkshopId, resource.VehicleId, resource.MechanicLeaderId, resource.Description);
    }
}