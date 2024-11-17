using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class PlansController(IPlanQueryService planQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all plans",
        Description = "Gets all plans",
        OperationId = "GetAllPlans")]
    [SwaggerResponse(200, "The plans were found", typeof(PlanResource))]
    [SwaggerResponse(404, "The plans were not found")]
    public async Task<IActionResult> GetAllPlans()
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery());
        var plansResource = plans.Select(PlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(plansResource);
    }
}