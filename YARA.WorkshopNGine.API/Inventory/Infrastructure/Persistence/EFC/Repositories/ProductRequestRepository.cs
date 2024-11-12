using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

public class ProductRequestRepository(AppDbContext context) : BaseRepository<ProductRequest>(context), IProductRequestRepository
{
    public async Task<IEnumerable<ProductRequest>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<ProductRequest>().Where(p => p.WorkshopId.Value == workshopId).ToListAsync();
    }

    public async Task<IEnumerable<ProductRequest>> FindAllByTaskIdAsync(long taskId)
    {
        return await Context.Set<ProductRequest>().Where(p => p.TaskId.Value == taskId).ToListAsync();
    }
}