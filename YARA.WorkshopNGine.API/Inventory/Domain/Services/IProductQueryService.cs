using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Services;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByWorkshopIdQuery query);
}