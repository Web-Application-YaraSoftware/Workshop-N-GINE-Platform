using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User> (context), IUserRepository
{
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }

    public bool VerifyPassword(string username, string password)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username) && user.Password.Equals(password));
    }

    public async Task<IEnumerable<User>> FindAllByRoleAndWorkshopAsync(long workshopId, long roleId)
    {
        return await Context.Set<User>()
            .Where(user => user.WorkshopId == workshopId && user.RoleId == roleId)
            .ToListAsync();
    }
}