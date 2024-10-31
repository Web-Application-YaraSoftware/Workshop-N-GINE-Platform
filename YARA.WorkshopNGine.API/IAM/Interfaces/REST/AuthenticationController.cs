using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.IAM.Domain.Services;
using YARA.WorkshopNGine.API.IAM.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.IAM.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Sign up a new user",
        OperationId = "SignUp"
    )]
    [SwaggerResponse(201, "User created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new {message = "User created successfully"});
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in an existing user",
        OperationId = "SignIn"
    )]
    [SwaggerResponse(200, "User authenticated successfully", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser);
        return Ok(authenticatedUserResource);
    }
}