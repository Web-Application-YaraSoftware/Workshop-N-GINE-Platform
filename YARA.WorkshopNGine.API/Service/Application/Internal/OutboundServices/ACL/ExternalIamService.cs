using YARA.WorkshopNGine.API.IAM.Interfaces.ACL;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.OutboundServices.ACL;

public class ExternalIamService(IIamContextFacade iamContextFacade)
{
    public async Task<IEnumerable<UserId>> FetchAllUsersByWorkshopAndRoleIsClientAsync(long workshopId)
    {
        var userIds = await iamContextFacade.FetchAllUsersByWorkshopAndRoleIsClient(workshopId);
        return userIds.Select(userId => new UserId(userId));
    }
    
    public async Task<IEnumerable<UserId>> FetchAllUsersByWorkshopAndRoleIsMechanicAsync(long workshopId)
    {
        var userIds = await iamContextFacade.FetchAllUsersByWorkshopAndRoleIsMechanic(workshopId);
        return userIds.Select(userId => new UserId(userId));
    }
    
    public async Task<UserId?> CreateUserWithRoleMechanicAsync(string username, string password, long workshopId)
    {
        var userId = await iamContextFacade.CreateUserWithRoleMechanic(username, password, workshopId);
        if (userId == 0) return await Task.FromResult<UserId?>(null);
        return new UserId(userId);
    }
    
    public async Task<UserId?> CreateUserWithRoleClientAsync(string username, string password, long workshopId)
    {
        var userId = await iamContextFacade.CreateUserWithRoleClient(username, password, workshopId);
        if (userId == 0) return await Task.FromResult<UserId?>(null);
        return new UserId(userId);
    }
}