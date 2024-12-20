﻿using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersByWorkshopAndRoleIsClientQuery query);
    
    Task<IEnumerable<User>> Handle(GetAllUsersByWorkshopAndRoleIsMechanicQuery query);
    
    Task<User> Handle(GetUserByIdQuery query);
}