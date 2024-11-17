namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record CreateProductRequestResource(int RequestedQuantity, long TaskId, long ProductId, long WorkshopId);