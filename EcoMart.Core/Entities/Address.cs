namespace EcoMart.Core.Entities
{
    /// <summary>
    /// Thực thể địa chỉ giao hàng - ánh xạ tới bảng Addresses trong cơ sở dữ liệu SQL Server.
    /// Một người dùng có thể có nhiều địa chỉ (quan hệ 1-Nhiều với bảng Users).
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Mã định danh duy nhất của mỗi địa chỉ.
        /// Kiểu INT, Khóa chính, tự động tăng (IDENTITY trong SQL Server).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Xác định địa chỉ này thuộc về người dùng nào (Quan hệ 1-Nhiều).
        /// Kiểu INT, Không được để trống, Khóa ngoại liên kết tới Id của bảng Users.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Tên của người sẽ nhận hàng tại địa chỉ này.
        /// Kiểu NVARCHAR(100), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string ReceiverName { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại của người nhận hàng.
        /// Kiểu VARCHAR(15), Không được để trống.
        /// </summary>
        public string ReceiverPhone { get; set; } = string.Empty;

        /// <summary>
        /// Số nhà, tên đường, số ngõ/ấp.
        /// Kiểu NVARCHAR(255), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string SpecificAddress { get; set; } = string.Empty;

        /// <summary>
        /// Tên Xã hoặc Phường.
        /// Kiểu NVARCHAR(100), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string Ward { get; set; } = string.Empty;

        /// <summary>
        /// Tên Quận hoặc Huyện.
        /// Kiểu NVARCHAR(100), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// Tên Tỉnh hoặc Thành phố.
        /// Kiểu NVARCHAR(100), Không được để trống. Hỗ trợ ký tự tiếng Việt.
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// Xác định đây có phải là địa chỉ giao hàng mặc định của người dùng hay không.
        /// Kiểu BIT, Mặc định là false (0 - địa chỉ phụ). 1 là mặc định, 0 là địa chỉ phụ.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        // ── Navigation Properties ──────────────────────────────────────────────

        /// <summary>
        /// Người dùng sở hữu địa chỉ này (quan hệ 1-Nhiều từ phía Address).
        /// </summary>
        public User User { get; set; } = null!;
    }
}
