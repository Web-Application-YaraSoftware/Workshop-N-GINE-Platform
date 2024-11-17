using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateTaskCommandFromResourceAssembler
{
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        return new CreateTaskCommand(resource.MechanicAssignedId, resource.Description);
    }
}