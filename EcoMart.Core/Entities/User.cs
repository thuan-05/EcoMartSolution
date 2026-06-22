namespace EcoMart.Core.Entities
{
    /// <summary>
    /// Thực thể người dùng - ánh xạ tới bảng Users trong cơ sở dữ liệu SQL Server.
    /// Lưu trữ thông tin cá nhân của người dùng (tách biệt với thông tin tài khoản đăng nhập).
    /// </summary>
    public class User
    {
        /// <summary>
        /// Mã định danh duy nhất của mỗi người dùng.
        /// Kiểu INT, Khóa chính, tự động tăng (IDENTITY trong SQL Server).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Họ và tên đầy đủ của người dùng.
        /// Kiểu NVARCHAR(100), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ email của người dùng để liên lạc hoặc khôi phục mật khẩu.
        /// Kiểu VARCHAR(100), Không được để trống, Không được trùng lặp.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại liên lạc của người dùng.
        /// Kiểu VARCHAR(15), Có thể để trống (nullable).
        /// </summary>
        public string? PhoneNumber { get; set; }

        // ── Navigation Properties ──────────────────────────────────────────────

        /// <summary>
        /// Thông tin tài khoản đăng nhập liên kết 1-1 với người dùng này.
        /// </summary>
        public Account? Account { get; set; }

        /// <summary>
        /// Danh sách địa chỉ giao hàng của người dùng này (quan hệ 1-Nhiều).
        /// </summary>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
