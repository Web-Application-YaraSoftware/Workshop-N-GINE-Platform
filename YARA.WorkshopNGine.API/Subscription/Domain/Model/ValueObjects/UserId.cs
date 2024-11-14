namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

public class UserId
{
    public UserId()
    {
        Value = 0;
    }
    
    public UserId(long value) : this()
    {
        if (value <= 0)
            throw new ArgumentException("UserId must be greater than 0");
        Value = value;
    }
    
    public long Value { get; private set; }
}