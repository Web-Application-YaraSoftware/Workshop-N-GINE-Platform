using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Device.Infrastructure.Persistence.EFC.Repositories;

public class IotDeviceRepository(AppDbContext context) : BaseRepository<IotDevice> (context), IIotDeviceRepository
{
    public async Task<IotDevice?> FindByVehicleIdAsync(long vehicleId)
    {
        return await Context.Set<IotDevice>()
            .Include(x => x.CodeList)
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);
    }
}