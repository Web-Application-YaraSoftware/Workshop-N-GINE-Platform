namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

public class ProductId
{
    public ProductId()
    {
        Value = 0;
    }
    
    public ProductId(long value) : this()
    {
        if (value <= 0)
            throw new ArgumentException("ProductId must be greater than 0");
        Value = value;
    }
    public long Value { get; private set; }
}