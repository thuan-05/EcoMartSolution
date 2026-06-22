using BCrypt.Net;
using EcoMart.Core.DTOs;
using EcoMart.Core.Entities;
using EcoMart.Core.Interfaces;

namespace EcoMart.Infrastructure.Services
{
    /// <summary>
    /// Bộ não xử lý toàn bộ nghiệp vụ xác thực người dùng.
    /// Đây là class THỰC SỰ thực thi hợp đồng IAuthService.
    /// </summary>
    public class AuthService : IAuthService
    {
        // _userRepository: "nhân viên kho" giúp AuthService đọc/ghi database
        // AuthService không tự vào database — nó nhờ UserRepository làm hộ
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Xử lý toàn bộ quy trình ĐĂNG KÝ tài khoản mới.
        /// Tạo đồng thời bản ghi Users (thông tin cá nhân) và Accounts (đăng nhập).
        /// </summary>
        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // BƯỚC 1: Kiểm tra username đã tồn tại chưa
            // Nếu đã có → báo lỗi ngay, không làm tiếp
            bool exists = await _userRepository.UsernameExistsAsync(dto.Username);
            if (exists)
            {
                return "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
            }

            // BƯỚC 2: Băm mật khẩu trước khi lưu
            // BCrypt.HashPassword("123456") → "$2a$11$abc..." (chuỗi ngẫu nhiên an toàn)
            // TUYỆT ĐỐI không lưu mật khẩu thô vào database!
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // BƯỚC 3: Tạo đối tượng User (thông tin cá nhân) để lưu vào bảng Users
            var newUser = new User
            {
                FullName = dto.FullName,
                Email    = dto.Email,
                PhoneNumber = dto.PhoneNumber,

                // Tạo Account (thông tin đăng nhập) lồng vào luôn
                // EF Core sẽ tự lưu cả 2 bảng trong 1 lần SaveChanges
                Account = new Account
                {
                    Username     = dto.Username,
                    PasswordHash = passwordHash,   // Lưu hash, không lưu mật khẩu gốc
                    Role         = "Customer",     // Mặc định mọi người đăng ký đều là Customer
                    IsActive     = true
                }
            };

            // BƯỚC 4: Lưu vào database (tự động lưu cả Users lẫn Accounts)
            await _userRepository.AddAsync(newUser);

            return "Đăng ký thành công! Chào mừng bạn đến với EcoMart.";
        }

        /// <summary>
        /// Xử lý toàn bộ quy trình ĐĂNG NHẬP.
        /// </summary>
        public async Task<string> LoginAsync(LoginDto dto)
        {
            // BƯỚC 1: Tìm user theo username trong bảng Accounts
            // Nếu không tìm thấy → tên đăng nhập sai
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null)
            {
                return "Tên đăng nhập hoặc mật khẩu không đúng.";
            }

            // BƯỚC 2: Kiểm tra mật khẩu
            // BCrypt.Verify("mật khẩu người dùng nhập", "hash trong database")
            // → Không giải mã hash, mà băm lại rồi so sánh
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.Account!.PasswordHash);
            if (!isPasswordCorrect)
            {
                // Cố tình trả về cùng thông báo như username sai
                // để hacker không biết đâu là username đúng, đâu là sai mật khẩu
                return "Tên đăng nhập hoặc mật khẩu không đúng.";
            }

            // BƯỚC 3: Đăng nhập thành công
            return $"Đăng nhập thành công! Chào mừng trở lại, {user.FullName}.";
        }
    }
}
