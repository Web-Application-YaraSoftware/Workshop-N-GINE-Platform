namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

public record UpdateProductCommand(string Name, string Description, int StockQuantity, int LowStockThreshold);