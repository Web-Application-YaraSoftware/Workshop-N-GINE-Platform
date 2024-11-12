using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProductRequestsController(IProductRequestCommandService productRequestCommandService, IProductRequestQueryService productRequestQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all product requests by workshop or by task",
        Description = "Gets all product requests for a given workshop id or task id",
        OperationId = "GetAllProductRequests")]
    [SwaggerResponse(200, "The product requests were found", typeof(IEnumerable<ProductRequestResource>))]
    [SwaggerResponse(404, "The product requests were not found")]
    public async Task<IActionResult> GetAllProductRequests([FromQuery] long workshopId, [FromQuery] long taskId)
    {
        IEnumerable<ProductRequest> productRequests;
        if (workshopId != 0)
        {
            var query = new GetAllProductRequestsByWorkshopIdQuery(workshopId);
            productRequests = await productRequestQueryService.Handle(query);
        }
        else if (taskId != 0)
        {
            var query = new GetAllProductRequestsByTaskIdQuery(taskId);
            productRequests = await productRequestQueryService.Handle(query);
        }
        else
        {
            return NotFound();
        }
        var resources = productRequests.Select(ProductRequestResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a product request",
        Description = "Creates a product request with a given quantity",
        OperationId = "CreateProductRequest")]
    [SwaggerResponse(201, "The product request was created", typeof(ProductRequestResource))]
    [SwaggerResponse(400, "The product request was not created")]
    public async Task<IActionResult> CreateProductRequest([FromBody] CreateProductRequestResource createProductRequestResource)
    {
        var createProductRequestCommand = CreateProductRequestCommandFromResourceAssembler.ToCommandFromResource(createProductRequestResource);
        var productRequest = await productRequestCommandService.Handle(createProductRequestCommand);
        if (productRequest == null) return BadRequest();
        var resource = ProductRequestResourceFromEntityAssembler.ToResourceFromEntity(productRequest);
        return CreatedAtAction(nameof(CreateProductRequest), new { productRequestId = resource.Id }, resource);
    }
    
    [HttpPut("{productRequestId:long}")]
    [SwaggerOperation(
        Summary = "Updates a product request",
        Description = "Updates a product request with a given identifier",
        OperationId = "UpdateProductRequest")]
    [SwaggerResponse(200, "The product request was updated", typeof(ProductRequestResource))]
    [SwaggerResponse(400, "The product request was not updated")]
    public async Task<IActionResult> UpdateProductRequest([FromRoute] long productRequestId, [FromBody] UpdateProductRequestResource updateProductRequestResource)
    {
        var updateProductRequestCommand = UpdateProductRequestCommandFromResourceAssembler.ToCommandFromResource(updateProductRequestResource);
        var productRequest = await productRequestCommandService.Handle(productRequestId, updateProductRequestCommand);
        if (productRequest == null) return BadRequest();
        var resource = ProductRequestResourceFromEntityAssembler.ToResourceFromEntity(productRequest);
        return Ok(resource);
    }
    
    [HttpPost("{productRequestId:long}/accept")]
    [SwaggerOperation(
        Summary = "Accepts a product request",
        Description = "Accepts a product request with a given identifier",
        OperationId = "AcceptProductRequest")]
    [SwaggerResponse(200, "The product request was accepted")]
    [SwaggerResponse(400, "The product request was not accepted")]
    public async Task<IActionResult> AcceptProductRequest([FromRoute] long productRequestId)
    {
        var acceptProductRequestCommand = new AcceptProductRequestCommand(productRequestId);
        var productRequest = await productRequestCommandService.Handle(acceptProductRequestCommand);
        if (productRequest == null || productRequest != productRequestId || productRequest == 0) return BadRequest();
        return Ok(new { message = "Product request accepted successfully" });
    }
    
    [HttpPost("{productRequestId:long}/reject")]
    [SwaggerOperation(
        Summary = "Rejects a product request",
        Description = "Rejects a product request with a given identifier",
        OperationId = "RejectProductRequest")]
    [SwaggerResponse(200, "The product request was rejected")]
    [SwaggerResponse(400, "The product request was not rejected")]
    public async Task<IActionResult> RejectProductRequest([FromRoute] long productRequestId)
    {
        var rejectProductRequestCommand = new RejectProductRequestCommand(productRequestId);
        var productRequest = await productRequestCommandService.Handle(rejectProductRequestCommand);
        if (productRequest == null || productRequest != productRequestId || productRequest == 0) return BadRequest();
        return Ok(new { message = "Product request rejected successfully" });
    }
}