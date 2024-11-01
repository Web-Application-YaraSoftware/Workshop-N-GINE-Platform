using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST.Transform;

public class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.FirstName, entity.LastName, entity.Dni, entity.Email, entity.Age, entity.Location, entity.UserId);
    }
}