using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);
    
    Task<Product?> Handle(long productId, UpdateProductCommand command);
    
    Task<long?> Handle(DeleteProductCommand command);
}