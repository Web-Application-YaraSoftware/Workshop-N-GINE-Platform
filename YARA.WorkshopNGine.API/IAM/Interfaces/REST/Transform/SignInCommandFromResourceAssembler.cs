using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}