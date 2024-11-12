namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

public class WorkshopId
{
    public WorkshopId()
    {
        Value = 0;
    }
    
    public WorkshopId(long value) : this()
    {
        if (value <= 0)
            throw new ArgumentException("WorkshopId must be greater than 0");
        Value = value;
    }
    public long Value { get; private set; }
}