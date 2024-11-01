using YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;
using YARA.WorkshopNGine.API.IAM.Domain.Services;

namespace YARA.WorkshopNGine.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<IEnumerable<long>> FetchAllUsersByRoleAndWorkshop(long roleId, long workshopId)
    {
        var getAllUsersByRoleAndWorkshop = new GetAllUsersByRoleAndWorkshopQuery(roleId, workshopId);
        var result = await userQueryService.Handle(getAllUsersByRoleAndWorkshop);
        return result.Select(user => user.Id);
    }
}