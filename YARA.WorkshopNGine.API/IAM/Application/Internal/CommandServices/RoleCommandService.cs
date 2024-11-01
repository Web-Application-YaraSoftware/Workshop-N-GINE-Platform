using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.IAM.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.CommandServices;

public class RoleCommandService(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRoleCommandService
{
    public async Task Handle(SeedRolesCommand command)
    {
        var tasks = (from Roles role in Enum.GetValues(typeof(Roles)) select ProcessRoleAsync(role)).ToList();
        await Task.WhenAll(tasks);
        await unitOfWork.CompleteAsync();
    }
    
    private async Task ProcessRoleAsync(Roles role)
    {
        try
        {
            if (!roleRepository.ExistsByName(role)) await roleRepository.AddAsync(new Role(role));
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while seeding role '{role}': {e.Message}", e);
        }
    }
}