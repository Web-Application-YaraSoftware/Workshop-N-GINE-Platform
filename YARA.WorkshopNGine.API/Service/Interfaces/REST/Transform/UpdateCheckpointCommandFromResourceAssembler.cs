using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class UpdateCheckpointCommandFromResourceAssembler
{
    public static UpdateCheckpointCommand ToCommandFromResource(UpdateCheckpointResource resource)
    {
        return new UpdateCheckpointCommand(resource.Name);
    }
}