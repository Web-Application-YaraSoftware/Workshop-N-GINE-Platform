using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Profiles.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public Task<Profile?> FindByDniAsync(int dni)
    {
        return Context.Set<Profile>().Where(p => p.Dni == dni).FirstOrDefaultAsync();
    }
    public Task<Profile?> FindByUserIdAsync(long userId)
    {
        return Context.Set<Profile>().Where(p => p.UserId == userId).FirstOrDefaultAsync();
    }
}