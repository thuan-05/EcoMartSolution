using EcoMart.Core.DTOs;

namespace EcoMart.Core.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
