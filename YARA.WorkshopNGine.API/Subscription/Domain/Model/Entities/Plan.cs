using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;

public class Plan
{
    public long Id { get; private set;}
    
    public decimal Price { get; private set; }
    
    public int DurationInMonths { get; private set; }
    
    public PlanTypes Type { get; private set; }
    
    public BillingCycles Cycle { get; private set; }
    
    public Limitations Limitations { get; private set; }
    
    private Plan(){}
    
    public Plan(long id, decimal price, int durationInMonths, PlanTypes type, BillingCycles cycle, Limitations limitations)
    {
        Id = id;
        Price = price;
        DurationInMonths = durationInMonths;
        Type = type;
        Cycle = cycle;
        Limitations = limitations;
    }
    
    public static Plan BasicPlan => new Plan(
        1,
        39.99m, 
        1, 
        PlanTypes.Basic, 
        BillingCycles.Monthly, 
        new Limitations(5, 10, 3, 3, 10, false)
    );

    public static Plan StandardPlan => new Plan(
        2,
        89.99m,
        1, 
        PlanTypes.Standard, 
        BillingCycles.Monthly, 
        new Limitations(25, 50, 10, 15, 40, true)
    );

    public static Plan PremiumPlan => new Plan(
        3,
        719.99m, 
        12, 
        PlanTypes.Premium, 
        BillingCycles.Annually, 
        new Limitations(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, true)
    );
    
    public string TypeToString() => Type switch
    {
        PlanTypes.Basic => "Basic",
        PlanTypes.Standard => "Standard",
        PlanTypes.Premium => "Premium",
        _ => "Unknown"
    };
    
    public string CycleToString() => Cycle switch
    {
        BillingCycles.Monthly => "Monthly",
        BillingCycles.Annually => "Annually",
        _ => "Unknown"
    };
}