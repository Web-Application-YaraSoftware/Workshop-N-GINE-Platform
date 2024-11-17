using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

public class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product entity)
    {
        return new ProductResource(entity.Id, entity.Name, entity.Description, entity.StockQuantity, entity.LowStockThreshold, entity.WorkshopId.Value);
    }
}