using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IVehicleQueryService
{
    Task<Vehicle?> Handle(GetVehicleByIdQuery query);
    Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesByUserIdQuery query);
}