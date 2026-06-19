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
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu dạng THÔ do người dùng nhập vào form.
        /// Lưu ý: đây là mật khẩu chưa mã hóa - sẽ được băm trước khi lưu vào database.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên đầy đủ của người dùng.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ giao hàng mặc định của người dùng.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
