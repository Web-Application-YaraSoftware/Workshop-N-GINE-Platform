using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    
    bool ExistsByUsername(string username);
    
    bool VerifyPassword(string username, string password);
    
    Task<IEnumerable<User>> FindAllByRoleAndWorkshopAsync(long workshopId, long roleId);
}