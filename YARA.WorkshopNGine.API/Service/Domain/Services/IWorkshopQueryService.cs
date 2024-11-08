using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IWorkshopQueryService
{
    Task<Workshop?> Handle(GetWorkshopByIdQuery query);
}