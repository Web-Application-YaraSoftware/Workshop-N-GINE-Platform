using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.CommandServices;

public class SubscriptionItemCommandService(ISubscriptionItemRepository subscriptionItemRepository, IPlanRepository planRepository, IUnitOfWork unitOfWork) : ISubscriptionItemCommandService
{
    public async Task<SubscriptionItem?> Handle(CreateSubscriptionItemWithTrialActivateCommand command)
    {
        if(subscriptionItemRepository.ExitsByWorkshopIdAndUserIdAndIsTrialAsync(command.WorkshopId.Value, command.UserId.Value))
            throw new Exception("User already has or had an active trial subscription");
        var basicPlan = await planRepository.FindByTypeIsBasicAsync();
        if(basicPlan == null)
            throw new Exception("Basic plan not found");
        var planId = new PlanId(basicPlan.Id);
        var subscription = new SubscriptionItem(command, planId);
        try
        {
            await subscriptionItemRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating a subscription item with trial activation: {e.Message}");
            return null;
        }
    }

    public async Task<SubscriptionItem?> Handle(CreateSubscriptionItemCommand command)
    {
        // TODO: Verify the workshop id is valid
        // TODO: Verify the user id is valid
        
        var plan = await planRepository.FindByIdAsync(command.PlanId.Value);
        if(plan == null)
            throw new Exception("Plan not found");
        
        var lastSubscription = await subscriptionItemRepository.FindLastByWorkshopIdAndUserIdAsync(command.WorkshopId.Value, command.UserId.Value);
        if(lastSubscription == null)
            throw new Exception("User does not have any subscription");
        
        if(lastSubscription.Status == SubscriptionStatuses.PendingActivation)
            throw new Exception("User already has a pending subscription");
        
        switch (lastSubscription.IsTrial)
        {
            case false when lastSubscription.Status == SubscriptionStatuses.Active:
                lastSubscription.CancelSubscription();
                break;
            case true:
                lastSubscription.EndTrial();
                break;
        }
        
        var subscription = new SubscriptionItem(command);
        
        var planDuration = plan.DurationInMonths;
        subscription.SetDuration(planDuration);
        
        try
        {
            subscriptionItemRepository.Update(lastSubscription);
            await subscriptionItemRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating a subscription item: {e.Message}");
            return null;
        }
    }

    public async Task<SubscriptionItem?> Handle(CancelSubscriptionItemCommand command)
    {
        var subscription = await subscriptionItemRepository.FindLastByWorkshopIdAsync(command.SubscriptionItemId);
        if(subscription == null)
            throw new Exception($"Subscription with {command.SubscriptionItemId} not found");
        switch (subscription.IsTrial)
        {
            case true:
                throw new Exception("Trial subscription cannot be canceled");
            case false when subscription.Status == SubscriptionStatuses.Canceled:
                throw new Exception("Subscription is already canceled");
            case false when subscription.Status == SubscriptionStatuses.Expired:
                throw new Exception("Subscription is already expired");
            default:
                subscription.CancelSubscription();
                try
                {
                    subscriptionItemRepository.Update(subscription);
                    await unitOfWork.CompleteAsync();
                    return subscription;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while canceling a subscription item: {e.Message}");
                    return null;
                }
                break;
        }
    }
}