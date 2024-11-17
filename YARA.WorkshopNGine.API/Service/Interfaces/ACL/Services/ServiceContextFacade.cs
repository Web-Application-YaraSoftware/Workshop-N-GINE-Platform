using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Services;

namespace YARA.WorkshopNGine.API.Service.Interfaces.ACL.Services;

public class ServiceContextFacade(IWorkshopCommandService workshopCommandService) : IServiceContextFacade
{
    public async Task<long> CreateWorkshop(string name)
    {
        var createWorkshopCommand = new CreateWorkshopCommand(name);
        var workshop = await workshopCommandService.Handle(createWorkshopCommand);
        return workshop?.Id ?? 0;
    }
}