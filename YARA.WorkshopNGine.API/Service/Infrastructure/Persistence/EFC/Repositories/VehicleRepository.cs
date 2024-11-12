using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Service.Infrastructure.Persistence.EFC.Repositories;

public class VehicleRepository(AppDbContext context) : BaseRepository<Vehicle> (context), IVehicleRepository
{
    public async Task<IEnumerable<Vehicle>> FindAllByUserIdAsync(long userId)
    {
        return await Context.Set<Vehicle>().Where(v => v.UserId == userId).ToListAsync();
    }

    public bool ExistsByLicensePlate(string licensePlate)
    {
        return Context.Set<Vehicle>().Any(v => v.LicensePlate == licensePlate);
    }
}