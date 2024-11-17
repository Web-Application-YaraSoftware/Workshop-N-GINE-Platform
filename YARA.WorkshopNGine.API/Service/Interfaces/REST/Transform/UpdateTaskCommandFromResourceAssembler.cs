using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class UpdateTaskCommandFromResourceAssembler
{
    public static UpdateTaskCommand ToCommandFromResource(UpdateTaskResource resource)
    {
        return new UpdateTaskCommand(resource.MechanicAssignedId, resource.Description);
    }
}