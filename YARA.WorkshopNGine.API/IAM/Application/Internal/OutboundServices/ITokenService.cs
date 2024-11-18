using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
}