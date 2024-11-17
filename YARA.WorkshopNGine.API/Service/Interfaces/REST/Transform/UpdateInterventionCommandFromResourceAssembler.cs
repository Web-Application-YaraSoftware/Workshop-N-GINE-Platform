using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class UpdateInterventionCommandFromResourceAssembler
{
    public static UpdateInterventionCommand ToCommandFromResource(UpdateInterventionResource resource)
    {
        return new UpdateInterventionCommand(
            resource.VehicleId,
            resource.MechanicLeaderId,
            resource.Description,
            (InterventionTypes)resource.Type,
            resource.ScheduledAt
        );
    }
}