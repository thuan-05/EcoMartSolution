using EcoMart.Core.DTOs;
using EcoMart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoMart.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>POST /api/auth/register</summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { message = "Đăng ký thành công! Chào mừng bạn đến với EcoMart." });
        }

        /// <summary>POST /api/auth/login</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            string fullName = await _authService.LoginAsync(dto);
            return Ok(new { message = $"Đăng nhập thành công! Chào mừng trở lại, {fullName}." });
        }
    }
}
