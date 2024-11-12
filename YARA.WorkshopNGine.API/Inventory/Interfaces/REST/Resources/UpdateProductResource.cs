namespace YARA.WorkshopNGine.API.Inventory.Interfaces.REST.Resources;

public record UpdateProductResource(string Name, string Description, int StockQuantity, int LowStockThreshold);