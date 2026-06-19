namespace EcoMart.Core.Entities
{
    /// <summary>
    /// Thực thể người dùng - ánh xạ tới bảng Users trong cơ sở dữ liệu SQL Server.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Khóa chính, tự động tăng (IDENTITY trong SQL Server).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên đăng nhập duy nhất của người dùng (tương ứng cột Username - NVARCHAR).
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Chuỗi mật khẩu đã được băm (hashed) - KHÔNG lưu mật khẩu dạng thô.
        /// Tương ứng cột PasswordHash - NVARCHAR trong SQL Server.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên đầy đủ của người dùng (tương ứng cột FullName - NVARCHAR).
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ giao hàng của người dùng (tương ứng cột Address - NVARCHAR).
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Vai trò của người dùng trong hệ thống, ví dụ: "Customer", "Admin".
        /// Tương ứng cột Role - NVARCHAR trong SQL Server.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
