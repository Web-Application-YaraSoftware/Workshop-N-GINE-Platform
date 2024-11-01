namespace YARA.WorkshopNGine.API.Profiles.interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<long> CreateProfile(string firstName, string lastName, int dni, string email, int age, string location, long userId);
    
    Task<long> UpdateProfile(long id, string firstName, string lastName, int dni, string email, int age, string location, int userId);
}