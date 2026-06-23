using EcoMart.Core.DTOs;

namespace EcoMart.Core.Interfaces
{
    public interface IAuthService
    {
     
        Task RegisterAsync(RegisterDto dto);

        Task<string> LoginAsync(LoginDto dto);
    }
}
