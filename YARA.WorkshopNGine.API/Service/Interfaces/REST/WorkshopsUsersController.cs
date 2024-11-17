using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/workshops/{workshopId:long}")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Workshops")]
public class WorkshopsUsersController(IWorkshopCommandService workshopCommandService, IWorkshopQueryService workshopQueryService)
    : ControllerBase
{
    [HttpGet("clients")]
    [SwaggerOperation(
        Summary = "Gets clients for a workshop",
        Description = "Gets clients for a workshop with a given identifier",
        OperationId = "GetClientsForWorkshop")]
    [SwaggerResponse(200, "The clients were found")]
    [SwaggerResponse(404, "The clients were not found")]
    public async Task<IActionResult> GetClientsForWorkshop([FromRoute] long workshopId)
    {
        var getClientsForWorkshopQuery = new GetAllUsersIdWithRoleClientQuery(workshopId);
        var clients = await workshopQueryService.Handle(getClientsForWorkshopQuery);
        var clientsId = clients.Select(c => c.Id);
        return Ok(clientsId);
    }
    
    [HttpGet("mechanics")]
    [SwaggerOperation(
        Summary = "Gets mechanics for a workshop",
        Description = "Gets mechanics for a workshop with a given identifier",
        OperationId = "GetMechanicsForWorkshop")]
    [SwaggerResponse(200, "The mechanics were found")]
    [SwaggerResponse(404, "The mechanics were not found")]
    public async Task<IActionResult> GetMechanicsForWorkshop([FromRoute] long workshopId)
    {
        var getMechanicsForWorkshopQuery = new GetAllUsersIdWithRoleMechanicQuery(workshopId);
        var mechanics = await workshopQueryService.Handle(getMechanicsForWorkshopQuery);
        var mechanicsId = mechanics.Select(m => m.Id);
        return Ok(mechanicsId);
    }
    
    [HttpPost("clients")]
    [SwaggerOperation(
        Summary = "Adds a client to a workshop",
        Description = "Adds a client to a workshop with a given information",
        OperationId = "AddClientToWorkshop")]
    [SwaggerResponse(201, "The client was added")]
    [SwaggerResponse(400, "The client was not added")]
    public async Task<IActionResult> AddClientToWorkshop([FromRoute] long workshopId, [FromBody] CreateClientResource resource)
    {
        var createClientCommand = CreateClientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var userId = await workshopCommandService.Handle(workshopId, createClientCommand);
        if (userId == null) return BadRequest();
        return CreatedAtAction(nameof(GetClientsForWorkshop), new { workshopId, userId = userId.Id }, userId.Id);
    }
    
    [HttpPost("mechanics")]
    [SwaggerOperation(
        Summary = "Adds a mechanic to a workshop",
        Description = "Adds a mechanic to a workshop with a given information",
        OperationId = "AddMechanicToWorkshop")]
    [SwaggerResponse(201, "The mechanic was added")]
    [SwaggerResponse(400, "The mechanic was not added")]
    public async Task<IActionResult> AddMechanicToWorkshop([FromRoute] long workshopId, [FromBody] CreateMechanicResource resource)
    {
        var createMechanicCommand = CreateMechanicCommandFromResourceAssembler.ToCommandFromResource(resource);
        var userId = await workshopCommandService.Handle(workshopId, createMechanicCommand);
        if (userId == null) return BadRequest();
        return CreatedAtAction(nameof(GetMechanicsForWorkshop), new { workshopId, userId = userId.Id }, userId.Id);
    }
}