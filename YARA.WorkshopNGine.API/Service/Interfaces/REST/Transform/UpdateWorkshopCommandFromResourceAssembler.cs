using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class UpdateWorkshopCommandFromResourceAssembler
{
    public static UpdateWorkshopCommand ToCommandFromResource(UpdateWorkshopResource resource)
    {
        return new UpdateWorkshopCommand(resource.Name);
    }
}