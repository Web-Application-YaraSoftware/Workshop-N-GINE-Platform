﻿using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.IAM.Domain.Services;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersByRoleAndWorkshopQuery query)
    {
        return await userRepository.FindAllByRoleAndWorkshopAsync(query.RoleId, query.WorkshopId);
    }
}