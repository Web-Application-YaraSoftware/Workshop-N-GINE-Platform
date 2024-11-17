using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/workshops/{workshopId:long}")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Workshops")]
public class WorkshopsInterventionsController(IInterventionQueryService interventionQueryService)
    : ControllerBase
{
    [HttpGet("interventions")]
    [SwaggerOperation(
        Summary = "Gets all interventions for a workshop",
        Description = "Gets all interventions for a workshop for a given identifier",
        OperationId = "GetInterventionsForWorkshop")]
    [SwaggerResponse(200, "The interventions were found", typeof(IEnumerable<InterventionResource>))]
    [SwaggerResponse(404, "The interventions were not found")]
    public async Task<IActionResult> GetInterventionsForWorkshop([FromRoute] long workshopId, [FromQuery] long mechanicLeaderId, [FromQuery] long mechanicAssitantId)
    {
        IEnumerable<Intervention> interventions;
        if(mechanicLeaderId != 0)
        {
            var query = new GetAllInterventionsByWorkshopAndMechanicLeader(mechanicLeaderId);
            interventions = await interventionQueryService.Handle(workshopId, query);
        }
        else if(mechanicAssitantId != 0)
        {
            var query = new GetAllInterventionsByWorkshopAndMechanicAssistant(mechanicAssitantId);
            interventions = await interventionQueryService.Handle(workshopId, query);
        }
        else
        {
            var query = new GetAllInterventionsByWorkshopQuery(workshopId);
            interventions = await interventionQueryService.Handle(query);
        }
        var resources = interventions.Select(InterventionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}