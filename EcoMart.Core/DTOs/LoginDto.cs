namespace EcoMart.Core.DTOs
{
    /// <summary>
    /// Tờ phiếu đăng nhập - chứa thông tin người dùng
    /// cần cung cấp để xác thực và truy cập vào hệ thống EcoMart.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Tên đăng nhập của tài khoản cần truy cập.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu dạng THÔ do người dùng nhập vào form đăng nhập.
        /// Lưu ý: sẽ được băm rồi so sánh với PasswordHash trong database,
        /// tuyệt đối không lưu mật khẩu này vào bất kỳ đâu.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
