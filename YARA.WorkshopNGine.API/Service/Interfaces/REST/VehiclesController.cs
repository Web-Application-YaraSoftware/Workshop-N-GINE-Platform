using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VehiclesController(IVehicleCommandService vehicleCommandService, IVehicleQueryService vehicleQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all vehicles by user",
        Description = "Gets all vehicles for a given user",
        OperationId = "GetAllVehiclesByUser")]
    [SwaggerResponse(200, "The vehicles were found", typeof(VehicleResource[]))]
    [SwaggerResponse(404, "The vehicles were not found")]
    public async Task<IActionResult> GetAllVehiclesByUser([FromQuery] long userId)
    {
        if (userId == 0) return BadRequest();
        var getAllVehiclesByUserIdQuery = new GetAllVehiclesByUserIdQuery(userId);
        var vehicles = await vehicleQueryService.Handle(getAllVehiclesByUserIdQuery);
        var vehicleResources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(vehicleResources);
    }
    
    [HttpGet("{vehicleId:long}")]
    [SwaggerOperation(
        Summary = "Gets a vehicle by id",
        Description = "Gets a vehicle for a given identifier",
        OperationId = "GetVehicleById")]
    [SwaggerResponse(200, "The vehicle was found", typeof(VehicleResource))]
    [SwaggerResponse(404, "The vehicle was not found")]
    public async Task<IActionResult> GetVehicleById([FromRoute] long vehicleId)
    {
        var getVehicleByIdQuery = new GetVehicleByIdQuery(vehicleId);
        var vehicle = await vehicleQueryService.Handle(getVehicleByIdQuery);
        if (vehicle == null) return NotFound();
        var vehicleResource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(vehicleResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a vehicle",
        Description = "Creates a vehicle with a given name",
        OperationId = "CreateVehicle")]
    [SwaggerResponse(201, "The vehicle was created", typeof(VehicleResource))]
    [SwaggerResponse(400, "The vehicle was not created")]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleResource createVehicleResource)
    {
        var createVehicleCommand = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(createVehicleResource);
        var vehicle = await vehicleCommandService.Handle(createVehicleCommand);
        if (vehicle == null) return BadRequest();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return CreatedAtAction(nameof(GetVehicleById), new { vehicleId = resource.Id }, resource);
    }
    
    [HttpPut("{vehicleId:long}")]
    [SwaggerOperation(
        Summary = "Updates a vehicle",
        Description = "Updates a vehicle with a given identifier",
        OperationId = "UpdateVehicle")]
    [SwaggerResponse(200, "The vehicle was updated", typeof(VehicleResource))]
    [SwaggerResponse(400, "The vehicle was not updated")]
    public async Task<IActionResult> UpdateVehicle(long vehicleId, UpdateVehicleResource updateVehicleResource)
    {
        var updateVehicleCommand = UpdateVehicleCommandFromResourceAssembler.ToCommandFromResource(updateVehicleResource);
        var vehicle = await vehicleCommandService.Handle(vehicleId, updateVehicleCommand);
        if (vehicle == null) return BadRequest();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle);
        return Ok(resource);
    }
    
    [HttpDelete("{vehicleId:long}")]
    [SwaggerOperation(
        Summary = "Deletes a vehicle",
        Description = "Deletes a vehicle with a given identifier",
        OperationId = "DeleteVehicle")]
    [SwaggerResponse(200, "The vehicle was deleted")]
    [SwaggerResponse(400, "The vehicle was not deleted")]
    public async Task<IActionResult> DeleteVehicle(long vehicleId)
    {
        var deleteVehicleCommand = new DeleteVehicleCommand(vehicleId);
        var vehicle = await vehicleCommandService.Handle(deleteVehicleCommand);
        if (vehicle == null) return BadRequest();
        return Ok();
    }
}