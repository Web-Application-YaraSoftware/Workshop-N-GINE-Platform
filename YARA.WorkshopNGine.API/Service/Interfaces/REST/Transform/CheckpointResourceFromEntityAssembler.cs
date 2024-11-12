using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CheckpointResourceFromEntityAssembler
{
    public static CheckpointResource ToResourceFromEntity(Checkpoint entity)
    {
        return new CheckpointResource(entity.Id, entity.Name, entity.TaskId);
    }
}