using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Services;

public interface IProductRequestCommandService
{
    Task<ProductRequest?> Handle(CreateProductRequestCommand command);
    
    Task<ProductRequest?> Handle(long productRequestId, UpdateProductRequestCommand command);
    
    Task<long?> Handle(AcceptProductRequestCommand command);
    
    Task<long?> Handle(RejectProductRequestCommand command);
}