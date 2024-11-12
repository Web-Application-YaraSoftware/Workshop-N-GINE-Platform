using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

public record UpdateProductRequestCommand(int RequestedQuantity, ProductId ProductId);