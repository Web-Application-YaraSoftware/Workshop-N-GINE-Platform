using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.Device.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Device.Domain.Services;
using YARA.WorkshopNGine.API.Device.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.Device.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.Device.Interfaces.REST;

[ApiController]
[Route( "api/v1/[controller]" )]
[Produces( MediaTypeNames.Application.Json )]
public class IotDevicesController(IIotDeviceQueryService iotDeviceQueryService)
    : ControllerBase
{
    [HttpGet( "{vehicleId:long}" )]
    [SwaggerResponse(200, "The Iot Device was found", typeof(IotDeviceResource))]
    public async Task<IActionResult> GetIotDeviceByVehicleId(long vehicleId)
    {
        var getIotDeviceByVehicleIdQuery = new GetIotDeviceByVehicleIdQuery( vehicleId );
        var iotDevice = await iotDeviceQueryService.Handle( getIotDeviceByVehicleIdQuery );
        if (iotDevice == null) return NotFound();
        var iotDeviceResources = IotDeviceResourceFromEntityAssembler.ToResourceFromEntity( iotDevice );
        return Ok(iotDeviceResources);
    }
}