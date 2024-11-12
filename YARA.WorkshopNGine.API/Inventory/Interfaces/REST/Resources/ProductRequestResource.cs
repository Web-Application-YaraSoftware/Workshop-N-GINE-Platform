namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record ProductRequestResource(long Id, int RequestedQuantity, long TaskId, long ProductId, long WorkshopId, string Status);