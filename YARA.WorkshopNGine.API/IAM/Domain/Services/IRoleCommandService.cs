using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.IAM.Domain.Services;

public interface IRoleCommandService
{
    Task Handle(SeedRolesCommand command);
}