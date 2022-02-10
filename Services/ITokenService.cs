using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}