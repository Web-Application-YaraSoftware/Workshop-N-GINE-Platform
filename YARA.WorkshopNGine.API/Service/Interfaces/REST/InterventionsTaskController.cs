using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/interventions/{interventionId:long}/tasks")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Interventions")]
public class InterventionsTaskController(IInterventionCommandService interventionCommandService, IInterventionQueryService interventionQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all tasks from an intervention",
        Description = "Gets all tasks from an intervention with a given identifier",
        OperationId = "GetAllTasksFromIntervention")]
    [SwaggerResponse(200, "The tasks were found", typeof(IEnumerable<TaskResource>))]
    [SwaggerResponse(404, "The tasks were not found")]
    public async Task<IActionResult> GetAllTasksFromIntervention([FromRoute] long interventionId, [FromQuery] long mechanicLeaderId)
    {
        IEnumerable<Task> tasks;
        if (mechanicLeaderId != 0)
        {
            var query = new GetAllTasksByInterventionAndMechanicAssignedQuery(mechanicLeaderId);
            tasks = await interventionQueryService.Handle(interventionId, query);
        }
        else
        {
            var query = new GetAllTasksByInterventionQuery();
            tasks = await interventionQueryService.Handle(interventionId, query);
        }
        var taskResources = tasks.Select(TaskResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(taskResources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Adds a task to an intervention",
        Description = "Adds a task to an intervention with a given information",
        OperationId = "AddTaskToIntervention")]
    [SwaggerResponse(201, "The task was added", typeof(TaskResource))]
    [SwaggerResponse(400, "The task was not added")]
    public async Task<IActionResult> AddTaskToIntervention([FromRoute] long interventionId, [FromBody] CreateTaskResource resource)
    {
        var createTaskCommand = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await interventionCommandService.Handle(interventionId, createTaskCommand);
        if (task == null) return BadRequest();
        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return CreatedAtAction(nameof(AddTaskToIntervention), new { interventionId, taskId = task.Id }, taskResource);
    }
    
    [HttpPut("{taskId:long}")]
    [SwaggerOperation(
        Summary = "Updates a task from an intervention",
        Description = "Updates a task from an intervention with a given identifier",
        OperationId = "UpdateTaskFromIntervention")]
    [SwaggerResponse(200, "The task was updated", typeof(TaskResource))]
    [SwaggerResponse(400, "The task was not updated")]
    public async Task<IActionResult> UpdateTaskFromIntervention([FromRoute] long interventionId, [FromRoute] long taskId, [FromBody] UpdateTaskResource resource)
    {
        var updateTaskCommand = UpdateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await interventionCommandService.Handle(interventionId, taskId, updateTaskCommand);
        if (task == null) return BadRequest();
        var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        return Ok(taskResource);
    }
    
    [HttpDelete("{taskId:long}")]
    [SwaggerOperation(
        Summary = "Deletes a task from an intervention",
        Description = "Deletes a task from an intervention with a given identifier",
        OperationId = "DeleteTaskFromIntervention")]
    [SwaggerResponse(204, "The task was deleted")]
    [SwaggerResponse(400, "The task was not deleted")]
    public async Task<IActionResult> DeleteTaskFromIntervention([FromRoute] long interventionId, [FromRoute] long taskId)
    {
        var deleteTaskCommand = new DeleteTaskCommand(taskId);
        var task = await interventionCommandService.Handle(interventionId, deleteTaskCommand);
        if (task == null) return BadRequest();
        return NoContent();
    }
    
    [HttpPost("{taskId:long}/in-progresses")]
    [SwaggerOperation(
        Summary = "Sets a task in-progress",
        Description = "Sets a task in-progress with a given identifier",
        OperationId = "SetTaskInProgress")]
    [SwaggerResponse(200, "The task was set in-progress")]
    [SwaggerResponse(400, "The task was not set in-progress")]
    public async Task<IActionResult> SetTaskInProgress([FromRoute] long interventionId, [FromRoute] long taskId)
    {
        var inProgressTaskCommand = new InProgressTaskCommand(taskId);
        var task = await interventionCommandService.Handle(interventionId, inProgressTaskCommand);
        if (task == null || task != taskId || task == 0) return BadRequest();
        return Ok(new { message = "Task set in-progress successfully" });
    }
    
    [HttpPost("{taskId:long}/confirmations")]
    [SwaggerOperation(
        Summary = "Completes a task",
        Description = "Completes a task with a given identifier",
        OperationId = "CompleteTask")]
    [SwaggerResponse(200, "The task was completed")]
    [SwaggerResponse(400, "The task was not completed")]
    public async Task<IActionResult> CompleteTask([FromRoute] long interventionId, [FromRoute] long taskId)
    {
        var completeTaskCommand = new CompleteTaskCommand(taskId);
        var task = await interventionCommandService.Handle(interventionId, completeTaskCommand);
        if (task == null || task != taskId || task == 0) return BadRequest();
        return Ok(new { message = "Task completed successfully" });
    }
}