using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public bool ExistsByName(Roles name)
    {
        return Context.Set<Role>().Any(role => role.Name.Equals(name.ToString()));
    }
}