namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record ProductResource(long Id, string Name, string Description, int StockQuantity, int LowStockThreshold, long WorkshopId);