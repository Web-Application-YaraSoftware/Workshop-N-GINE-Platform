using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IWorkshopQueryService
{
    Task<Workshop?> Handle(GetWorkshopByIdQuery query);
    
    Task<IEnumerable<UserId>> Handle(GetAllUsersIdWithRoleClientQuery query);
    
    Task<IEnumerable<UserId>> Handle(GetAllUsersIdWithRoleMechanicQuery query);
}