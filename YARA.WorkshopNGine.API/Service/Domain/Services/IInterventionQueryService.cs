using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IInterventionQueryService
{
    Task<Intervention?> Handle(GetInterventionByIdQuery query);
    
    Task<IEnumerable<Intervention>> Handle(GetAllInterventionsByWorkshopQuery query);
}