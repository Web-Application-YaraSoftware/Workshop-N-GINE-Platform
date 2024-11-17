using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class WorkshopResourceFromEntityAssembler
{
    public static WorkshopResource ToResourceFromEntity(Workshop entity)
    {
        return new WorkshopResource(entity.Id, entity.Name);
    }
}