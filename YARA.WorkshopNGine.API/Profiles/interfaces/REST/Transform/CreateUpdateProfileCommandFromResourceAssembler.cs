using YARA.WorkshopNGine.API.Profiles.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST.Transform;

public class CreateUpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(resource.Id, resource.FirstName, resource.LastName, resource.Dni, resource.Email, resource.Age, resource.Location, resource.UserId);
    }
}