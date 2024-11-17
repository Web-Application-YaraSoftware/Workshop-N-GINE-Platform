using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Transform;

public class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        return new PlanResource(entity.Id, entity.Price, entity.DurationInMonths, entity.TypeToString(), entity.CycleToString(), entity.Limitations.MaxClients, entity.Limitations.MaxMechanics, entity.Limitations.MaxActiveInterventions, entity.Limitations.MaxTasksPerMechanic, entity.Limitations.MaxItems, entity.Limitations.MetricsAvailable);
    }
}