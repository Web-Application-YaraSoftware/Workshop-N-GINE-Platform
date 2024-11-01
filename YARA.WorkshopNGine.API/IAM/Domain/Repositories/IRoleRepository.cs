using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    bool ExistsByName(Roles name);
}