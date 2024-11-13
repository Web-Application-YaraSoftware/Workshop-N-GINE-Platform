using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Billing.Domain.Services;
using YARA.WorkshopNGine.API.Billing.Interfaces.Resources;
using YARA.WorkshopNGine.API.Billing.Interfaces.Transform;

namespace YARA.WorkshopNGine.API.Billing.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class InvoicesController(IInvoiceCommandService invoiceCommandService, IInvoiceQueryService invoiceQueryService): ControllerBase
{
    [HttpPost]
    [SwaggerOperation (Summary = "Create Invoice", Description = "Create an invoice", OperationId = "CreateInvoice")]
    [SwaggerResponse(201, "The invoice was created", typeof(InvoiceResource))]
    public async Task<IActionResult> CreateInvoice(CreateInvoiceResource resource)
    {
        var createInvoiceCommand = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var invoice = await invoiceCommandService.Handle(createInvoiceCommand);
        if(invoice == null) { return BadRequest(); }
        var invoiceResource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return CreatedAtAction(nameof(CreateInvoice), new {invoiceId = invoice.Id}, invoiceResource);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get all invoices", Description = "Get all invoices", OperationId = "GetAllInvoices")]
    [SwaggerResponse(200, "The invoices were found", typeof(IEnumerable<InvoiceResource>))]
    public async Task<IActionResult> GetAllInvoicesByWorkshopId([FromQuery] long workshopId)
    {
        var getAllInvoiceByWorkshopIdQuery = new GetAllInvoiceByWorkshopIdQuery(workshopId);
        var invoices = await invoiceQueryService.Handle(getAllInvoiceByWorkshopIdQuery);
        if(invoices == null) { return NotFound(); }
        var invoiceResources = invoices.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(invoiceResources);
        
    }
    
}