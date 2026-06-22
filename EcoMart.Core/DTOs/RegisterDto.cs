namespace EcoMart.Core.DTOs
{
    /// <summary>
    /// Tờ phiếu đăng ký tài khoản - chứa đúng những thông tin
    /// người dùng cần điền khi tạo tài khoản mới trên EcoMart.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Tên đăng nhập người dùng tự chọn (phải là duy nhất trong hệ thống).
        /// Sẽ được lưu vào bảng Accounts.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu dạng THÔ do người dùng nhập vào form.
        /// Lưu ý: đây là mật khẩu chưa mã hóa - sẽ được băm trước khi lưu vào database.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên đầy đủ của người dùng. Sẽ được lưu vào bảng Users.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ email của người dùng. Sẽ được lưu vào bảng Users.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại liên lạc (có thể để trống). Sẽ được lưu vào bảng Users.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
