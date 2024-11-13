namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

public class PlanId
{
    public PlanId()
    {
        Value = 0;
    }
    
    public PlanId(long value) : this()
    {
        if (value <= 0)
            throw new ArgumentException("PlanId must be greater than 0");
        Value = value;
    }
    public long Value { get; private set; }
}