using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.CommandServices;

public class SubscriptionItemCommandService(ISubscriptionItemRepository subscriptionItemRepository, IUnitOfWork unitOfWork) : ISubscriptionItemCommandService
{
    public async Task<SubscriptionItem?> Handle(CreateSubscriptionItemCommand command)
    {
        var previousSubscription = await subscriptionItemRepository.FindByWorkshopIdAndStatusIsActiveAsync(command.WorkshopId.Value);
        // TODO: Verify the previous subscription is the default plan "Free" and if it is, change the status to "Canceled"
        // TODO: Verify the previous subscription is not the default plan "Free" and if it is, throw an exception
        // TODO: Verify the plan id is valid
        // TODO: Verify the workshop id is valid
        // TODO: Get the duration of the plan
        var planDuration = 1;
        var subscription = new SubscriptionItem(command);
        subscription.SetDuration(planDuration);
        try
        {
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
        var subscription = await subscriptionItemRepository.FindByWorkshopIdAndStatusIsActiveAsync(command.SubscriptionItemId);
        if(subscription == null)
            throw new Exception($"Active subscription with {command.SubscriptionItemId} not found");
        // TODO: Verify the current plan is not the default plan "Free"
        subscription.CancelSubscription();
        // TODO: Create a new subscription item with the same workshop id and the default plan "Free"
        // TODO: Get the default plan id from the database
        var defaultPlanId = new PlanId(1);
        var newSubscription = new SubscriptionItem(subscription.WorkshopId, defaultPlanId);
        try
        {
            subscriptionItemRepository.Update(subscription);
            await subscriptionItemRepository.AddAsync(newSubscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while canceling a subscription item: {e.Message}");
            return null;
        }
    }
}