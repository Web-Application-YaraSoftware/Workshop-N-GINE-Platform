using YARA.WorkshopNGine.API.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace YARA.WorkshopNGine.API.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCryptNet.Verify(password, hashedPassword);
    }
}