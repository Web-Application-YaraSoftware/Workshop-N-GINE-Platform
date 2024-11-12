using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;

public class ProductRequest
{
    public long Id { get;}
    
    public int RequestedQuantity { get; private set; }
    
    public TaskId TaskId { get; private set; }
    
    public ProductId ProductId { get; private set; }
    
    public WorkshopId WorkshopId { get; private set; }
    
    public ProductRequestStatuses Status { get; private set; }

    public ProductRequest()
    {
        this.RequestedQuantity = 0;
        this.TaskId = new TaskId();
        this.ProductId = new ProductId();
        this.WorkshopId = new WorkshopId();
        this.Status = ProductRequestStatuses.Pending;
    }

    public ProductRequest(CreateProductRequestCommand command) : this()
    {
        this.RequestedQuantity = command.RequestedQuantity;
        this.TaskId = command.TaskId;
        this.ProductId = command.ProductId;
        this.WorkshopId = command.WorkshopId;
    }
    
    public void Update(UpdateProductRequestCommand command)
    {
        this.RequestedQuantity = command.RequestedQuantity;
        this.ProductId = command.ProductId;
    }
    
    public void Accept()
    {
        this.Status = ProductRequestStatuses.Accepted;
    }
    
    public void Reject()
    {
        this.Status = ProductRequestStatuses.Rejected;
    }
    
    public string StatusToString()
    {
        return this.Status switch
        {
            ProductRequestStatuses.Pending => "Pending",
            ProductRequestStatuses.Accepted => "Accepted",
            ProductRequestStatuses.Rejected => "Rejected",
            _ => "Pending"
        };
    }
}