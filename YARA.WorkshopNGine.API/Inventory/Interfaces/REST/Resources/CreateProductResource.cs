namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record CreateProductResource(string Name, string Description, int StockQuantity, int LowStockThreshold, long WorkshopId);