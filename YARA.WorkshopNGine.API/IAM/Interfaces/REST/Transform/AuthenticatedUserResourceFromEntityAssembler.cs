using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, entity.RoleId, entity.WorkshopId);
    }
};