namespace YARA.WorkshopNGine.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<IEnumerable<long>> FetchAllUsersByWorkshopAndRoleIsMechanic(long workshopId);
    
    Task<IEnumerable<long>> FetchAllUsersByWorkshopAndRoleIsClient(long workshopId);
    
    Task<long> CreateUserWithRoleMechanic(string username, string password, long workshopId); 
    
    Task<long> CreateUserWithRoleClient(string username, string password, long workshopId);
}