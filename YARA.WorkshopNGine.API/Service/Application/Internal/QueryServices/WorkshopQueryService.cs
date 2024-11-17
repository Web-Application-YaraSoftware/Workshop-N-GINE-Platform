using YARA.WorkshopNGine.API.Service.Application.Internal.OutboundServices.ACL;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;

public class WorkshopQueryService(IWorkshopRepository workshopRepository, ExternalIamService externalIamService) : IWorkshopQueryService
{
    public async Task<Workshop?> Handle(GetWorkshopByIdQuery query)
    {
        return await workshopRepository.FindByIdAsync(query.WorkshopId);
    }

    public async Task<IEnumerable<UserId>> Handle(GetAllUsersIdWithRoleClientQuery query)
    {
        return await externalIamService.FetchAllUsersByWorkshopAndRoleIsClientAsync(query.WorkshopId);
    }

    public Task<IEnumerable<UserId>> Handle(GetAllUsersIdWithRoleMechanicQuery query)
    {
        return externalIamService.FetchAllUsersByWorkshopAndRoleIsMechanicAsync(query.WorkshopId);
    }
}