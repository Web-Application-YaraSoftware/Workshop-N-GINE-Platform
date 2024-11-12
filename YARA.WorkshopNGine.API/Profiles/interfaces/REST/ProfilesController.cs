using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Profiles.Domain.Services;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Profiles.interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfilesController(IProfileQueryService profileQueryService, IProfileCommandService profileCommandService)
    : ControllerBase
{
    [HttpGet("{profileId:long}")]
    public async Task<IActionResult> GetProfileById([FromRoute] long profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile([FromQuery] int userId, [FromQuery] int dni)
    {
        Profile? profile;
        if (userId != 0)
        {
            var getProfileByUserIdQuery = new GetProfileByUserIdQuery(userId);
            profile = await profileQueryService.Handle(getProfileByUserIdQuery);
            
        }
        else if (dni != 0)
        {
            var getProfileByDniQuery = new GetProfileByDniQuery(dni);
            profile = await profileQueryService.Handle(getProfileByDniQuery);
        }
        else
        {
            return BadRequest();
        }
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpPut("{profileId:long}")]
    public async Task<IActionResult> UpdateProfile(long profileId, UpdateProfileResource resource)
    {
        var updateProfileCommand = CreateUpdateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(profileId, updateProfileCommand);
        if (profile == null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
}