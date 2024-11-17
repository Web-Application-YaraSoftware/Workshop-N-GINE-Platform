using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Application.Internal.QueryServices;

public class ProductRequestQueryService(IProductRequestRepository productRequestRepository, IUnitOfWork unitOfWork) : IProductRequestQueryService
{
    public async Task<IEnumerable<ProductRequest>> Handle(GetAllProductRequestsByWorkshopIdQuery query)
    {
        return await productRequestRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }

    public async Task<IEnumerable<ProductRequest>> Handle(GetAllProductRequestsByTaskIdQuery query)
    {
        return await productRequestRepository.FindAllByTaskIdAsync(query.TaskId);
    }
}