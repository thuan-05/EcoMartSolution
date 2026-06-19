using EcoMart.Core.DTOs;

namespace EcoMart.Core.Interfaces
{
    /// <summary>
    /// Hợp đồng cho lớp xử lý nghiệp vụ xác thực người dùng.
    /// Định nghĩa những GÌ hệ thống có thể làm liên quan đến
    /// đăng ký và đăng nhập - không quan tâm thực hiện như thế nào.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Xử lý toàn bộ quy trình đăng ký tài khoản mới:
        /// 1. Kiểm tra username đã tồn tại chưa
        /// 2. Băm mật khẩu
        /// 3. Lưu user mới vào database
        /// Trả về thông báo kết quả (thành công hoặc lý do thất bại).
        /// </summary>
        Task<string> RegisterAsync(RegisterDto dto);

        /// <summary>
        /// Xử lý toàn bộ quy trình đăng nhập:
        /// 1. Tìm user theo username
        /// 2. Băm mật khẩu vừa nhập và so sánh với hash trong database
        /// 3. Xác nhận kết quả
        /// Trả về thông báo kết quả (thành công hoặc lý do thất bại).
        /// </summary>
        Task<string> LoginAsync(LoginDto dto);
    }
}
