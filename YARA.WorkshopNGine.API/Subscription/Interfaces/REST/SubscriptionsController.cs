using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionsController(ISubscriptionItemCommandService subscriptionItemCommandService, ISubscriptionItemQueryService subscriptionItemQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all subscription items",
        Description = "Gets all subscription items",
        OperationId = "GetAllSubscriptionItems")]
    [SwaggerResponse(200, "The subscription items were found", typeof(SubscriptionItemResource))]
    [SwaggerResponse(404, "The subscription items were not found")]
    public async Task<IActionResult> GetAllSubscriptionItemsByWorkshopId([FromQuery] long workshopId)
    {
        if(workshopId == 0) return BadRequest();
        var getAllSubscriptionItemsByWorkshopIdQuery = new GetAllSubscriptionItemsByWorkshopIdQuery(workshopId);
        var subscriptionItems = await subscriptionItemQueryService.Handle(getAllSubscriptionItemsByWorkshopIdQuery);
        var subscriptionItemsResource = subscriptionItems.Select(SubscriptionItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(subscriptionItemsResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a subscription item",
        Description = "Creates a subscription item with a given workshop identifier",
        OperationId = "CreateSubscriptionItem")]
    [SwaggerResponse(201, "The subscription item was created", typeof(SubscriptionItemResource))]
    [SwaggerResponse(400, "The subscription item was not created")]
    public async Task<IActionResult> CreateSubscriptionItem([FromBody] CreateSubscriptionItemResource createSubscriptionItemResource)
    {
        var createSubscriptionItemCommand = CreateSubscriptionItemCommandFromResourceAssembler.ToCommandFromResource(createSubscriptionItemResource);
        var subscriptionItem = await subscriptionItemCommandService.Handle(createSubscriptionItemCommand);
        if (subscriptionItem == null) return BadRequest();
        var resource = SubscriptionItemResourceFromEntityAssembler.ToResourceFromEntity(subscriptionItem);
        return CreatedAtAction(nameof(CreateSubscriptionItem), new { workshopId = resource.WorkshopId }, resource);
    }
    
    [HttpPost("{subscriptionId:long}/cancel")]
    [SwaggerOperation(
        Summary = "Cancels a subscription item",
        Description = "Cancels a subscription item with a given workshop identifier",
        OperationId = "CancelSubscriptionItem")]
    [SwaggerResponse(200, "The subscription item was cancelled", typeof(SubscriptionItemResource))]
    [SwaggerResponse(400, "The subscription item was not cancelled")]
    public async Task<IActionResult> CancelSubscriptionItem([FromRoute] long subscriptionId)
    {
        var cancelSubscriptionItemCommand = new CancelSubscriptionItemCommand(subscriptionId);
        var subscriptionItem = await subscriptionItemCommandService.Handle(cancelSubscriptionItemCommand);
        if (subscriptionItem == null) return BadRequest();
        var resource = SubscriptionItemResourceFromEntityAssembler.ToResourceFromEntity(subscriptionItem);
        return Ok(new {message = "Subscription '" + resource.Id + "' was cancelled successfully"});
    }
}