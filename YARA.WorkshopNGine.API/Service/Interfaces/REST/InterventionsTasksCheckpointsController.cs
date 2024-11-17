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
[Route("api/v1/interventions/{interventionId:long}/tasks/{taskId:long}/checkpoints")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Interventions")]
public class InterventionsTasksCheckpointsController(IInterventionCommandService interventionCommandService, IInterventionQueryService interventionQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all checkpoints from a task",
        Description = "Gets all checkpoints from a task with a given identifier",
        OperationId = "GetAllCheckpointsFromTask")]
    [SwaggerResponse(200, "The checkpoints were found", typeof(IEnumerable<CheckpointResource>))]
    [SwaggerResponse(404, "The checkpoints were not found")]
    public async Task<IActionResult> GetAllCheckpointsFromTask([FromRoute] long interventionId, [FromRoute] long taskId)
    {
        var getAllCheckpointsByTasKAndInterventionQuery = new GetAllCheckpointsByTaskAndInterventionQuery();
        var checkpoints = await interventionQueryService.Handle(interventionId, taskId, getAllCheckpointsByTasKAndInterventionQuery);
        var checkpointResources = checkpoints.Select(CheckpointResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(checkpointResources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Adds a checkpoint to a task",
        Description = "Adds a checkpoint to a task with a given information",
        OperationId = "AddCheckpointToTask")]
    [SwaggerResponse(201, "The checkpoint was added", typeof(CheckpointResource))]
    [SwaggerResponse(400, "The checkpoint was not added")]
    public async Task<IActionResult> AddCheckpointToTask([FromRoute] long interventionId, [FromRoute] long taskId, [FromBody] CreateCheckpointResource resource)
    {
        var createCheckpointCommand = CreateCheckpointCommandFromResourceAssembler.ToCommandFromResource(resource);
        var checkpoint = await interventionCommandService.Handle(interventionId, taskId, createCheckpointCommand);
        if (checkpoint == null) return BadRequest();
        var checkpointResource = CheckpointResourceFromEntityAssembler.ToResourceFromEntity(checkpoint);
        return CreatedAtAction(nameof(AddCheckpointToTask), new { interventionId, taskId, checkpointId = checkpoint.Id }, checkpointResource);
    }
    
    [HttpPut("{checkpointId:long}")]
    [SwaggerOperation(
        Summary = "Updates a checkpoint from a task",
        Description = "Updates a checkpoint from a task with a given information",
        OperationId = "UpdateCheckpointFromTask")]
    [SwaggerResponse(200, "The checkpoint was updated", typeof(CheckpointResource))]
    [SwaggerResponse(400, "The checkpoint was not updated")]
    public async Task<IActionResult> UpdateCheckpointFromTask([FromRoute] long interventionId, [FromRoute] long taskId, [FromRoute] long checkpointId, [FromBody] UpdateCheckpointResource resource)
    {
        var updateCheckpointCommand = UpdateCheckpointCommandFromResourceAssembler.ToCommandFromResource(resource);
        var checkpoint = await interventionCommandService.Handle(interventionId, taskId, checkpointId, updateCheckpointCommand);
        if (checkpoint == null) return BadRequest();
        var checkpointResource = CheckpointResourceFromEntityAssembler.ToResourceFromEntity(checkpoint);
        return Ok(checkpointResource);
    }
    
    [HttpDelete("{checkpointId:long}")]
    [SwaggerOperation(
        Summary = "Removes a checkpoint from a task",
        Description = "Removes a checkpoint from a task with a given identifier",
        OperationId = "RemoveCheckpointFromTask")]
    [SwaggerResponse(204, "The checkpoint was removed")]
    [SwaggerResponse(400, "The checkpoint was not removed")]
    public async Task<IActionResult> RemoveCheckpointFromTask([FromRoute] long interventionId, [FromRoute] long taskId, [FromRoute] long checkpointId)
    {
        var deleteCheckpointCommand = new DeleteCheckpointCommand(checkpointId);
        var checkpoint = await interventionCommandService.Handle(interventionId, taskId, deleteCheckpointCommand);
        if (checkpoint == null) return BadRequest();
        return NoContent();
    }
}