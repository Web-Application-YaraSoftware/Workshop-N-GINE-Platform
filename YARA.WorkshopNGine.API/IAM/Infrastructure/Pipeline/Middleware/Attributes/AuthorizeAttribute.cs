using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;

namespace YARA.WorkshopNGine.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping Authorization");
            return;
        }
        // Verify if user is authenticated
        var user = (User?)context.HttpContext.Items["User"];
        // If user is not authenticated, return 401 Unauthorized
        if (user is null) context.Result = new UnauthorizedResult();
    }
}