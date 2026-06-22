using EcoMart.Core.DTOs;
using EcoMart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoMart.WebAPI.Controllers
{
    /// <summary>
    /// Cánh cửa API cho tính năng xác thực người dùng.
    /// Nhận HTTP request từ bên ngoài (Postman, app web)
    /// và chuyển giao cho AuthService xử lý.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // _authService: "bộ não" xử lý nghiệp vụ đăng ký/đăng nhập
        // Controller chỉ nhận và trả dữ liệu, KHÔNG tự xử lý logic
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Endpoint đăng ký tài khoản mới.
        /// HTTP POST: /api/auth/register
        /// Body (JSON): { "username": "...", "password": "...", "fullName": "...", "email": "...", "phoneNumber": "..." }
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Giao toàn bộ xử lý cho AuthService
            string result = await _authService.RegisterAsync(dto);

            // Kiểm tra kết quả để trả về HTTP status code phù hợp
            if (result.Contains("thành công"))
            {
                // 200 OK: đăng ký thành công
                return Ok(new { message = result });
            }

            // 400 Bad Request: có lỗi (ví dụ: username đã tồn tại)
            return BadRequest(new { message = result });
        }

        /// <summary>
        /// Endpoint đăng nhập.
        /// HTTP POST: /api/auth/login
        /// Body (JSON): { "username": "...", "password": "..." }
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Giao toàn bộ xử lý cho AuthService
            string result = await _authService.LoginAsync(dto);

            // Kiểm tra kết quả để trả về HTTP status code phù hợp
            if (result.Contains("thành công"))
            {
                // 200 OK: đăng nhập thành công
                return Ok(new { message = result });
            }

            // 401 Unauthorized: sai tên đăng nhập hoặc mật khẩu
            return Unauthorized(new { message = result });
        }
    }
}
