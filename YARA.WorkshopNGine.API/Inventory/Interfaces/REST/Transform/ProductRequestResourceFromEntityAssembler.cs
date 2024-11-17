using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

public class ProductRequestResourceFromEntityAssembler
{
    public static ProductRequestResource ToResourceFromEntity(ProductRequest entity)
    {
        return new ProductRequestResource(entity.Id, entity.RequestedQuantity, entity.TaskId.Value, entity.ProductId.Value, entity.WorkshopId.Value, entity.StatusToString());
    }
}