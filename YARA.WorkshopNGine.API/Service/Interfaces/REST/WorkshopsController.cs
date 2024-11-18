using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class WorkshopsController(IWorkshopCommandService workshopCommandService, IWorkshopQueryService workshopQueryService)
    : ControllerBase
{
    [HttpGet("{workshopId:long}")]
    [SwaggerOperation(
        Summary = "Gets a workshop by id",
        Description = "Gets a workshop for a given identifier",
        OperationId = "GetWorkshopById")]
    [SwaggerResponse(200, "The workshop was found", typeof(WorkshopResource))]
    [SwaggerResponse(404, "The workshop was not found")]
    public async Task<IActionResult> GetWorkshopById([FromRoute] long workshopId)
    {
        var getWorkshopByIdQuery = new GetWorkshopByIdQuery(workshopId);
        var workshop = await workshopQueryService.Handle(getWorkshopByIdQuery);
        if (workshop == null) return NotFound();
        var workshopResource = WorkshopResourceFromEntityAssembler.ToResourceFromEntity(workshop);
        return Ok(workshopResource);
    }
    
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates a workshop",
        Description = "Creates a workshop with a given name",
        OperationId = "CreateWorkshop")]
    [SwaggerResponse(201, "The workshop was created", typeof(WorkshopResource))]
    [SwaggerResponse(400, "The workshop was not created")]
    public async Task<IActionResult> CreateWorkshop([FromBody] CreateWorkshopResource createWorkshopResource)
    {
        var createWorkshopCommand = CreateWorkshopCommandFromResourceAssembler.ToCommandFromResource(createWorkshopResource);
        var workshop = await workshopCommandService.Handle(createWorkshopCommand);
        if (workshop == null) return BadRequest();
        var resource = WorkshopResourceFromEntityAssembler.ToResourceFromEntity(workshop);
        return CreatedAtAction(nameof(GetWorkshopById), new { workshopId = resource.Id }, resource);
    }
    
    [HttpPut("{workshopId:long}")]
    [SwaggerOperation(
        Summary = "Updates a workshop",
        Description = "Updates a workshop with a given identifier",
        OperationId = "UpdateWorkshop")]
    [SwaggerResponse(200, "The workshop was updated", typeof(WorkshopResource))]
    [SwaggerResponse(400, "The workshop was not updated")]
    public async Task<IActionResult> UpdateWorkshop([FromRoute] long workshopId, [FromBody] UpdateWorkshopResource updateWorkshopResource)
    {
        var updateWorkshopCommand = UpdateWorkshopCommandFromResourceAssembler.ToCommandFromResource(updateWorkshopResource);
        var workshop = await workshopCommandService.Handle(workshopId, updateWorkshopCommand);
        if (workshop == null) return BadRequest();
        var resource = WorkshopResourceFromEntityAssembler.ToResourceFromEntity(workshop);
        return Ok(resource);
    }
}