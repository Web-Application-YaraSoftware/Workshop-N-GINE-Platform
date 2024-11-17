namespace YARA.WorkshopNGine.API.Inventory.Domain.Model.ValueObjects;

public class TaskId
{
    public TaskId()
    {
        Value = 0;
    }
    
    public TaskId(long value) : this()
    {
        if (value <= 0)
            throw new ArgumentException("TaskId must be greater than 0");
        Value = value;
    }
    
    public long Value { get; private set; }
}