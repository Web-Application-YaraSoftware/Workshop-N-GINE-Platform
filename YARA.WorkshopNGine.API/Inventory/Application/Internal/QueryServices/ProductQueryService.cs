using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Application.Internal.QueryServices;

public class ProductQueryService (IProductRepository productRepository, IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork) : IProductQueryService
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByWorkshopIdQuery query)
    {
        return await productRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }
}