using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.IAM.Domain.Services;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersByWorkshopAndRoleIsClientQuery query)
    {
        return await userRepository.FindAllByRoleAndWorkshopAsync(query.WorkshopId, (long)Roles.Client);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersByWorkshopAndRoleIsMechanicQuery query)
    {
        return await userRepository.FindAllByRoleAndWorkshopAsync(query.WorkshopId, (long)Roles.Mechanic);
    }

    public async Task<User> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.UserId);
    }
}