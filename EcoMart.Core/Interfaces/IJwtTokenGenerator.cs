using EcoMart.Core.Entities;

namespace EcoMart.Core.Interfaces
{
    
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
