using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

public record CreateProductRequestCommand(int RequestedQuantity, TaskId TaskId, ProductId ProductId, WorkshopId WorkshopId);