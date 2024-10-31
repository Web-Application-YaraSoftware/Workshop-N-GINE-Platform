using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Profiles.Domain.Services;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfilesController(IProfileQueryService profileQueryService, IProfileCommandService profileCommandService)
    : ControllerBase
{
    [HttpGet("profile/{profileId:long}")]
    public async Task<IActionResult> GetProfileById(long profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpGet("{dni:int}")]
    public async Task<IActionResult> GetProfileByDni(int dni)
    {
        var getProfileByDniQuery = new GetProfileByDniQuery(dni);
        var profile = await profileQueryService.Handle(getProfileByDniQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetProfileByUserId(long userId)
    {
        var getProfileByUserIdQuery = new GetProfileByUserIdQuery(userId);
        var profile = await profileQueryService.Handle(getProfileByUserIdQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
}