using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Transform;

public class UpdateProductRequestCommandFromResourceAssembler
{
    public static UpdateProductRequestCommand ToCommandFromResource(UpdateProductRequestResource resource)
    {
        var productId = new ProductId(resource.ProductId);
        return new UpdateProductRequestCommand(resource.RequestedQuantity, productId);
    }
}