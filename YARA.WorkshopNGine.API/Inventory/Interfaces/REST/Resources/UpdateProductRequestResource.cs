namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record UpdateProductRequestResource(int RequestedQuantity, long ProductId);