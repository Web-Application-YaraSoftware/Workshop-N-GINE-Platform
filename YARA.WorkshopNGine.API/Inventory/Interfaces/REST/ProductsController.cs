using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProductsController(IProductCommandService productCommandService, IProductQueryService productQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all products by workshop",
        Description = "Gets all products for a given workshop id",
        OperationId = "GetAllProductsByWorkshop")]
    [SwaggerResponse(200, "The products were found", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(404, "The products were not found")]
    public async Task<IActionResult> GetAllProductsByWorkshop([FromQuery] long workshopId)
    {
        if(workshopId == 0) return NotFound();
        var getAllProductsByWorkshopIdQuery = new GetAllProductsByWorkshopIdQuery(workshopId);
        var products = await productQueryService.Handle(getAllProductsByWorkshopIdQuery);
        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a product",
        Description = "Creates a product with a given name",
        OperationId = "CreateProduct")]
    [SwaggerResponse(201, "The product was created", typeof(ProductResource))]
    [SwaggerResponse(400, "The product was not created")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource createProductResource)
    {
        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(createProductResource);
        var product = await productCommandService.Handle(createProductCommand);
        if (product == null) return BadRequest();
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return CreatedAtAction(nameof(CreateProduct), new { productId = resource.Id }, resource);
    }
    
    [HttpPut("{productId:long}")]
    [SwaggerOperation(
        Summary = "Updates a product",
        Description = "Updates a product with a given identifier",
        OperationId = "UpdateProduct")]
    [SwaggerResponse(200, "The product was updated", typeof(ProductResource))]
    [SwaggerResponse(400, "The product was not updated")]
    public async Task<IActionResult> UpdateProduct(long productId, UpdateProductResource updateProductResource)
    {
        var updateProductCommand = UpdateProductCommandFromResourceAssembler.ToCommandFromResource(updateProductResource);
        var product = await productCommandService.Handle(productId, updateProductCommand);
        if (product == null) return BadRequest();
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(resource);
    }
    
    [HttpDelete("{productId:long}")]
    [SwaggerOperation(
        Summary = "Deletes a product",
        Description = "Deletes a product with a given identifier",
        OperationId = "DeleteProduct")]
    [SwaggerResponse(200, "The product was deleted")]
    [SwaggerResponse(400, "The product was not deleted")]
    public async Task<IActionResult> DeleteProduct(long productId)
    {
        var deleteProductCommand = new DeleteProductCommand(productId);
        var product = await productCommandService.Handle(deleteProductCommand);
        if (product == null) return BadRequest();
        return Ok();
    }
}