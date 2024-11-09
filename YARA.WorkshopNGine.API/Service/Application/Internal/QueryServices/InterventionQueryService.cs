using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

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

    public async Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionQuery query)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        var tasks = intervention?.Tasks ?? new List<Task>();
        return tasks;
    }

    public async Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionAndMechanicAssignedQuery query)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        var tasks = intervention?.FindAllTasksByMechanicAssignedId(query.MechanicAssignedId) ?? new List<Task>();
        return tasks;
    }
}