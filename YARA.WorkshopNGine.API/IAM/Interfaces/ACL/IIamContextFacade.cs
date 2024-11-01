namespace YARA.WorkshopNGine.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<IEnumerable<long>> FetchAllUsersByRoleAndWorkshop(long roleId, long workshopId);
}