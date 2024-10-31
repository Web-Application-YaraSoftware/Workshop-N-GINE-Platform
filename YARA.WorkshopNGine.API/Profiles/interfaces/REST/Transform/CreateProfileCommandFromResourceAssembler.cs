using YARA.WorkshopNGine.API.Profiles.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST.Transform;

public class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(resource.FirstName, resource.LastName, resource.Dni, resource.Email,
            resource.Age, resource.Location, resource.UserId);
    }
}