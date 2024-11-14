namespace YARA.WorkshopNGine.API.Service.Interfaces.ACL;

public interface IServiceContextFacade
{
    Task<long> CreateWorkshop(string name);
}