using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.CommandServices;

public class PlanCommandService(IPlanRepository planRepository , IUnitOfWork unitOfWork) : IPlanCommandService
{
    public async Task Handle(SeedPlansCommand command)
    {
        await DeleteAllPlansAsync();
        var basicPlan = Plan.BasicPlan;
        var standardPlan = Plan.StandardPlan;
        var premiumPlan = Plan.PremiumPlan;
        var plans = new List<Plan> {basicPlan, standardPlan, premiumPlan};
        await Task.WhenAll(plans.Select(ProcessPlanAsync));
        await unitOfWork.CompleteAsync();
    }
    private async Task DeleteAllPlansAsync()
    {
        var existingPlan = await planRepository.ListAsync();
        foreach (var plan in existingPlan)
        {
            planRepository.Remove(plan);
        }
    }
    private async Task ProcessPlanAsync(Plan plan)
    {
        try
        {
            await planRepository.AddAsync(plan);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while seeding plan '{plan.Id}': {e.Message}", e);
        }
    }
}