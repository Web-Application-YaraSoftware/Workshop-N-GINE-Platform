using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;

public class Product
{
    public long Id { get;}
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public int StockQuantity { get; private set; }
    
    public int LowStockThreshold { get; private set; }
    
    public WorkshopId WorkshopId { get; private set; }

    public Product()
    {
        this.Name = string.Empty;
        this.Description = string.Empty;
        this.StockQuantity = 0;
        this.LowStockThreshold = 0;
        this.WorkshopId = new WorkshopId();
    }
    
    public Product(CreateProductCommand command) : this()
    {
        Name = command.Name;
        Description = command.Description;
        StockQuantity = command.StockQuantity;
        LowStockThreshold = command.LowStockThreshold;
        WorkshopId = command.WorkshopId;
    }
    
    public void Update(UpdateProductCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        StockQuantity = command.StockQuantity;
        LowStockThreshold = command.LowStockThreshold;
    }
    
    public bool IsAvailableRequest(int quantity)
    {
        return StockQuantity >= quantity;
    }
    
    public void Request(int quantity)
    {
        StockQuantity -= quantity;
    }
}