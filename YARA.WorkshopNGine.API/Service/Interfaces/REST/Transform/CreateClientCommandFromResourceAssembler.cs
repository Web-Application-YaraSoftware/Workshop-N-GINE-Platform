using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateClientCommandFromResourceAssembler
{
    public static CreateClientCommand ToCommandFromResource(CreateClientResource resource)
    {
        return new CreateClientCommand(resource.FirstName, resource.LastName, resource.Dni, resource.Email, resource.Age, resource.Location);
    }
}