using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;

public class WorkshopQueryService(IWorkshopRepository workshopRepository) : IWorkshopQueryService
{
    public async Task<Workshop?> Handle(GetWorkshopByIdQuery query)
    {
        return await workshopRepository.FindByIdAsync(query.WorkshopId);
    }
}