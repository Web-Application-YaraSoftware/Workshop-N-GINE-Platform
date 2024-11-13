using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.QueryServices;

public class PlanQueryService(IPlanRepository planRepository, IUnitOfWork unitOfWork) : IPlanQueryService
{
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query)
    {
        return await planRepository.ListAsync();
    }
}