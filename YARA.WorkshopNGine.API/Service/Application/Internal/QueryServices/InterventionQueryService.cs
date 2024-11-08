using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;

public class InterventionQueryService(IInterventionRepository interventionRepository, IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork) : IInterventionQueryService
{
    public async Task<Intervention?> Handle(GetInterventionByIdQuery query)
    {
        return await interventionRepository.FindByIdAsync(query.InterventionId);
    }

    public Task<IEnumerable<Intervention>> Handle(GetAllInterventionsByWorkshopQuery query)
    {
        return interventionRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }
}