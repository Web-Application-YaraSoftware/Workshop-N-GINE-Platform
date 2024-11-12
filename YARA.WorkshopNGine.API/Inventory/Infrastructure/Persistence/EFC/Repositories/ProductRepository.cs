using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<Product>().Where(p => p.WorkshopId.Value == workshopId).ToListAsync();
    }

    public bool ExistsById(long productId)
    {
        return Context.Set<Product>().Any(p => p.Id == productId);
    }
}