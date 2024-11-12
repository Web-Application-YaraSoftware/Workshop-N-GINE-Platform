using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;

public class VehicleQueryService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork) : IVehicleQueryService
{
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query)
    {
        return await vehicleRepository.FindByIdAsync(query.VehicleId);
    }

    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesByUserIdQuery query)
    {
        return await vehicleRepository.FindAllByUserIdAsync(query.UserId);
    }
}