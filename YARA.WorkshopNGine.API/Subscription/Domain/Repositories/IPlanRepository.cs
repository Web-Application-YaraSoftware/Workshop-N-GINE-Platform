using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

public interface IPlanRepository : IBaseRepository<Plan>
{
    bool ExistsByIdAsync(long id);
    
    Task<Plan?> FindByTypeIsBasicAsync();
    
    Task<Plan?> FindByTypeIsStandardAsync();
    
    Task<Plan?> FindByTypeIsPremiumAsync();
}