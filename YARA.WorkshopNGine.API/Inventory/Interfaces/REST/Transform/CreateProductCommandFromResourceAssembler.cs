using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

public class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        var workshopId = new WorkshopId(resource.WorkshopId);
        return new CreateProductCommand(resource.Name, resource.Description, resource.StockQuantity, resource.LowStockThreshold, workshopId);
    }
}