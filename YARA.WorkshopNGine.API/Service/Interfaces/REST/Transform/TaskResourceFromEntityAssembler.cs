using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class TaskResourceFromEntityAssembler
{
    public static TaskResource ToResourceFromEntity(Task entity)
    {
        return new TaskResource(entity.Id, entity.MechanicAssignedId, entity.Description, entity.StatusToString(), entity.InterventionId);
    }
}