using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Repositories;

public interface IProductRequestRepository : IBaseRepository<ProductRequest>
{
    Task<IEnumerable<ProductRequest>> FindAllByWorkshopIdAsync(long workshopId);
    
    Task<IEnumerable<ProductRequest>> FindAllByTaskIdAsync(long taskId);
}