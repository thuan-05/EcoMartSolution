namespace EcoMart.Core.Entities
{
    /// <summary>
    /// Thực thể tài khoản - ánh xạ tới bảng Accounts trong cơ sở dữ liệu SQL Server.
    /// Lưu trữ thông tin đăng nhập, tách biệt với thông tin cá nhân của người dùng.
    /// Quan hệ 1-1 với bảng Users thông qua AccountId = Users.Id.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Mã tài khoản, liên kết 1-1 với thông tin cá nhân.
        /// Kiểu INT, Khóa chính, đồng thời là Khóa ngoại liên kết tới Id của bảng Users.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Tên tài khoản dùng để đăng nhập vào hệ thống.
        /// Kiểu VARCHAR(50), Không được để trống, Không được trùng lặp.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu đã được mã hóa băm để đảm bảo an toàn, không lưu mật khẩu thô.
        /// Kiểu VARCHAR(MAX), Không được để trống.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Quyền hạn của tài khoản trong hệ thống (ví dụ: Admin, Customer).
        /// Kiểu VARCHAR(20), Không được để trống.
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Trạng thái tài khoản: 1 là đang hoạt động, 0 là bị khóa.
        /// Kiểu BIT, Mặc định là true (1 - đang hoạt động).
        /// </summary>
        public bool IsActive { get; set; } = true;

        // ── Navigation Properties ──────────────────────────────────────────────

        /// <summary>
        /// Thông tin cá nhân của người dùng liên kết với tài khoản này (quan hệ 1-1).
        /// </summary>
        public User User { get; set; } = null!;
    }
}
