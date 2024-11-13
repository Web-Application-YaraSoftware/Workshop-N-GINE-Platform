using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;

public class SubscriptionItem
{
    public long Id { get;}
    
    public WorkshopId WorkshopId { get; private set; }
    
    public PlanId PlanId { get; private set; }
    
    public SubscriptionStatuses Status { get; private set; }
    
    public DateTime StartedAt { get; private set; }
    
    public DateTime? EndedAt { get; private set; }
    
    public DateTime? CancelledAt { get; private set; }
    
    public SubscriptionItem()
    {
        this.WorkshopId = new WorkshopId();
        this.PlanId = new PlanId();
        this.Status = SubscriptionStatuses.PendingActivation;
        this.StartedAt = DateTime.UtcNow;
        this.EndedAt = null;
        this.CancelledAt = null;
    }
    
    public SubscriptionItem(WorkshopId workshopId, PlanId planId) : this()
    {
        WorkshopId = workshopId;
        PlanId = planId;
    }

    public SubscriptionItem(CreateSubscriptionItemCommand itemCommand) : this()
    {
        WorkshopId = itemCommand.WorkshopId;
        PlanId = itemCommand.PlanId;
    }
    
    public void EndSubscription()
    {
        Status = SubscriptionStatuses.Expired;
    }
    
    public void ActivateSubscription()
    {
        Status = SubscriptionStatuses.Active;
    }
    
    public void CancelSubscription()
    {
        Status = SubscriptionStatuses.Canceled;
        EndedAt = DateTime.UtcNow;
    }
    
    public void SetDuration(int durationInMonths)
    {
        EndedAt = StartedAt.AddMonths(durationInMonths);
    }
    
    public string StatusToString()
    {
        return Status switch
        {
            SubscriptionStatuses.PendingActivation => "Pending Activation",
            SubscriptionStatuses.Active => "Active",
            SubscriptionStatuses.Expired => "Inactive",
            SubscriptionStatuses.Canceled => "Canceled",
            _ => "Unknown"
        };
    }
}