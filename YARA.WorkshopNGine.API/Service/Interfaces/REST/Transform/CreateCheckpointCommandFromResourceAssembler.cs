using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateCheckpointCommandFromResourceAssembler
{
    public static CreateCheckpointCommand ToCommandFromResource(CreateCheckpointResource resource)
    {
        return new CreateCheckpointCommand(resource.Name);
    }
}