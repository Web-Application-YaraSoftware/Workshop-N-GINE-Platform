using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

public class CreateProductRequestCommandFromResourceAssembler
{
    public static CreateProductRequestCommand ToCommandFromResource(CreateProductRequestResource resource)
    {
        var taskId = new TaskId(resource.TaskId);
        var productId = new ProductId(resource.ProductId);
        var workshopId = new WorkshopId(resource.WorkshopId);
        return new CreateProductRequestCommand(resource.RequestedQuantity, taskId, productId, workshopId);
    }
}