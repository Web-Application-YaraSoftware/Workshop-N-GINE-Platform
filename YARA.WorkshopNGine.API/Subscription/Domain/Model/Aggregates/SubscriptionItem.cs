using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;

public partial class SubscriptionItem
{
    public long Id { get;}
    
    public WorkshopId WorkshopId { get; private set; }
    
    public PlanId PlanId { get; private set; }
    
    public UserId UserId { get; private set; }
    
    public SubscriptionStatuses Status { get; private set; }
    
    public DateTime? StartedAt { get; private set; }
    
    public DateTime? EndedAt { get; private set; }
    
    public DateTime? CancelledAt { get; private set; }
    
    public bool IsTrial { get; private set; }
    
    public DateTime? TrialEndsAt { get; private set; }
    
    public SubscriptionItem()
    {
        this.WorkshopId = new WorkshopId();
        this.PlanId = new PlanId();
        this.UserId = new UserId();
        this.Status = SubscriptionStatuses.PendingActivation;
        this.StartedAt = null;
        this.EndedAt = null;
        this.CancelledAt = null;
        this.IsTrial = false;
        this.TrialEndsAt = null;
    }
    
    public SubscriptionItem(WorkshopId workshopId, UserId userId, PlanId planId) : this()
    {
        WorkshopId = workshopId;
        UserId = userId;
        PlanId = planId;
    }

    public SubscriptionItem(CreateSubscriptionItemCommand itemCommand) : this()
    {
        WorkshopId = itemCommand.WorkshopId;
        UserId = itemCommand.UserId;
        PlanId = itemCommand.PlanId;
    }
    
    public SubscriptionItem(CreateSubscriptionItemWithTrialActivate command, PlanId planId) : this()
    {
        WorkshopId = command.WorkshopId;
        UserId = command.UserId;
        PlanId = planId;
        StartTrial();
    }
    
    public void StartTrial()
    {
        Status = SubscriptionStatuses.Active;
        StartedAt = DateTime.UtcNow;
        TrialEndsAt = StartedAt?.AddDays(30);
        IsTrial = true;
    }
    
    public bool IsTrialExpired()
    {
        return IsTrial && TrialEndsAt.HasValue && TrialEndsAt.Value < DateTime.UtcNow;
    }
    
    public void EndTrial()
    {
        Status = SubscriptionStatuses.Expired;
        CancelledAt = DateTime.UtcNow;
    }
    
    public void EndSubscription()
    {
        Status = SubscriptionStatuses.Expired;
    }
    
    public void ActivateSubscription()
    {
        StartedAt = DateTime.UtcNow;
        Status = SubscriptionStatuses.Active;
    }
    
    public void CancelSubscription()
    {
        Status = SubscriptionStatuses.Canceled;
        CancelledAt = DateTime.UtcNow;
    }
    
    public void SetDuration(int durationInMonths)
    {
        EndedAt = StartedAt?.AddMonths(durationInMonths);
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