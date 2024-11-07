using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;
using YARA.WorkshopNGine.API.IAM.Domain.Services;

namespace YARA.WorkshopNGine.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserQueryService userQueryService, IUserCommandService userCommandService) : IIamContextFacade
{
    public async Task<IEnumerable<long>> FetchAllUsersByWorkshopAndRoleIsMechanic(long workshopId)
    {
        var getAllUsersByWorkshopAndRoleIsMechanic = new GetAllUsersByWorkshopAndRoleIsMechanicQuery(workshopId);
        var result = await userQueryService.Handle(getAllUsersByWorkshopAndRoleIsMechanic);
        return result.Select(user => user.Id);
    }

    public async Task<IEnumerable<long>> FetchAllUsersByWorkshopAndRoleIsClient(long workshopId)
    {
        var getAllUsersByWorkshopAndRoleIsClient = new GetAllUsersByWorkshopAndRoleIsClientQuery(workshopId);
        var result = await userQueryService.Handle(getAllUsersByWorkshopAndRoleIsClient);
        return result.Select(user => user.Id);
    }

    public async Task<long> CreateUserWithRoleMechanic(string username, string password, long workshopId)
    {
        var createUserWithRoleMechanic = new CreateUserWithRoleMechanicCommand(username, password, workshopId);
        var result = await userCommandService.Handle(createUserWithRoleMechanic);
        return result.Id;
    }

    public async Task<long> CreateUserWithRoleClient(string username, string password, long workshopId)
    {
        var createUserWithRoleClient = new CreateUserWithRoleClientCommand(username, password, workshopId);
        var result = await userCommandService.Handle(createUserWithRoleClient);
        return result.Id;
    }
}