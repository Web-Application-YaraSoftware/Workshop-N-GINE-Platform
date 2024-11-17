using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class CreateMechanicCommandFromResourceAssembler
{
    public static CreateMechanicCommand ToCommandFromResource(CreateMechanicResource resource)
    {
        return new CreateMechanicCommand(resource.FirstName, resource.LastName, resource.Dni, resource.Email, resource.Age, resource.Location);
    }
}