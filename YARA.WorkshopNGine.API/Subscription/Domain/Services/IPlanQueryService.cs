using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Services;

public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query);
}