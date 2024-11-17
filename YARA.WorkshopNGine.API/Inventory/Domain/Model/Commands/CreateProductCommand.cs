using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

public record CreateProductCommand(string Name, string Description, int StockQuantity, int LowStockThreshold, WorkshopId WorkshopId);