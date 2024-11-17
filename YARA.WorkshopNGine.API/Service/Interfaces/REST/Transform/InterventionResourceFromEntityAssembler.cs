using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class InterventionResourceFromEntityAssembler
{
    public static InterventionResource ToResourceFromEntity(Intervention entity)
    {
        return new InterventionResource(entity.Id, entity.VehicleId, entity.MechanicLeaderId, entity.Description, entity.TypeToString(), entity.StatusToString(), entity.ScheduledAt, entity.StartedAt, entity.FinishedAt);
    }
}