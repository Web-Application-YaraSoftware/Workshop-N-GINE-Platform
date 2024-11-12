using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Services;

public interface IProductRequestQueryService
{
    Task<IEnumerable<ProductRequest>> Handle(GetAllProductRequestsByWorkshopIdQuery query);
    
    Task<IEnumerable<ProductRequest>> Handle(GetAllProductRequestsByTaskIdQuery query);
}