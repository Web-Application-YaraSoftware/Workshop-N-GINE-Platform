using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class InterventionsController(IInterventionCommandService interventionCommandService, IInterventionQueryService interventionQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all interventions",
        Description = "Gets all interventions",
        OperationId = "GetAllInterventions")]
    [SwaggerResponse(200, "The interventions were found", typeof(IEnumerable<InterventionResource>))]
    [SwaggerResponse(404, "The interventions were not found")]
    public async Task<IActionResult> GetAllInterventions([FromQuery] long vehicleId)
    {
        if (vehicleId == 0) return BadRequest();
        var getAllInterventionsByVehicleIdQuery = new GetAllInterventionsByVehicleQuery(vehicleId);
        var interventions = await interventionQueryService.Handle(getAllInterventionsByVehicleIdQuery);
        var resources =  interventions.Select(InterventionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{interventionId:long}")]
    [SwaggerOperation(
        Summary = "Gets an intervention by id",
        Description = "Gets an intervention for a given identifier",
        OperationId = "GetInterventionById")]
    [SwaggerResponse(200, "The intervention was found", typeof(InterventionResource))]
    [SwaggerResponse(404, "The intervention was not found")]
    public async Task<IActionResult> GetInterventionById(long interventionId)
    {
        var getInterventionByIdQuery = new GetInterventionByIdQuery(interventionId);
        var intervention = await interventionQueryService.Handle(getInterventionByIdQuery);
        if (intervention == null) return NotFound();
        var interventionResource = InterventionResourceFromEntityAssembler.ToResourceFromEntity(intervention);
        return Ok(interventionResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates an intervention",
        Description = "Creates an intervention with a given name",
        OperationId = "CreateIntervention")]
    [SwaggerResponse(201, "The intervention was created", typeof(InterventionResource))]
    [SwaggerResponse(400, "The intervention was not created")]
    public async Task<IActionResult> CreateIntervention([FromBody] CreateInterventionResource createInterventionResource)
    {
        var createInterventionCommand = CreateInterventionCommandFromResourceAssembler.ToCommandFromResource(createInterventionResource);
        var intervention = await interventionCommandService.Handle(createInterventionCommand);
        if (intervention == null) return BadRequest();
        var resource = InterventionResourceFromEntityAssembler.ToResourceFromEntity(intervention);
        return CreatedAtAction(nameof(GetInterventionById), new { interventionId = resource.Id }, resource);
    }
    
    [HttpPut("{interventionId:long}")]
    [SwaggerOperation(
        Summary = "Updates an intervention",
        Description = "Updates an intervention with a given identifier",
        OperationId = "UpdateIntervention")]
    [SwaggerResponse(200, "The intervention was updated", typeof(InterventionResource))]
    [SwaggerResponse(400, "The intervention was not updated")]
    public async Task<IActionResult> UpdateIntervention(long interventionId, UpdateInterventionResource updateInterventionResource)
    {
        var updateInterventionCommand = UpdateInterventionCommandFromResourceAssembler.ToCommandFromResource(updateInterventionResource);
        var intervention = await interventionCommandService.Handle(interventionId, updateInterventionCommand);
        if (intervention == null) return BadRequest();
        var resource = InterventionResourceFromEntityAssembler.ToResourceFromEntity(intervention);
        return Ok(resource);
    }
    
    [HttpPost("{interventionId:long}/in-progresses")]
    [SwaggerOperation(
        Summary = "Sets an intervention in-progress",
        Description = "Sets an intervention in-progress with a given identifier",
        OperationId = "SetInterventionInProgress")]
    [SwaggerResponse(200, "The intervention was set in-progress")]
    [SwaggerResponse(400, "The intervention was not set in-progress")]
    public async Task<IActionResult> SetInterventionInProgress(long interventionId)
    {
        var inProgressInterventionCommand = new InProgressInterventionCommand(interventionId);
        var intervention = await interventionCommandService.Handle(inProgressInterventionCommand);
        if (intervention == null || intervention != interventionId || intervention == 0) return BadRequest();
        return Ok(new { message = "Intervention set in-progress successfully" });
    }
    
    [HttpPost("{interventionId:long}/confirmations")]
    [SwaggerOperation(
        Summary = "Completes an intervention",
        Description = "Completes an intervention with a given identifier",
        OperationId = "CompleteIntervention")]
    [SwaggerResponse(200, "The intervention was completed")]
    [SwaggerResponse(400, "The intervention was not completed")]
    public async Task<IActionResult> CompleteIntervention(long interventionId)
    {
        var completeInterventionCommand = new CompleteInterventionCommand(interventionId);
        var intervention = await interventionCommandService.Handle(completeInterventionCommand);
        if (intervention == null || intervention != interventionId || intervention == 0) return BadRequest();
        return Ok(new { message = "Intervention completed successfully" });
    }
    
    [HttpPost("{interventionId:long}/cancellations")]
    [SwaggerOperation(
        Summary = "Cancels an intervention",
        Description = "Cancels an intervention with a given identifier",
        OperationId = "CancelIntervention")]
    [SwaggerResponse(200, "The intervention was cancelled")]
    [SwaggerResponse(400, "The intervention was not cancelled")]
    public async Task<IActionResult> CancelIntervention(long interventionId)
    {
        var cancelInterventionCommand = new CancelInterventionCommand(interventionId);
        var intervention = await interventionCommandService.Handle(cancelInterventionCommand);
        if (intervention == null || intervention != interventionId || intervention == 0) return BadRequest();
        return Ok(new { message = "Intervention cancelled successfully" });
    }
}