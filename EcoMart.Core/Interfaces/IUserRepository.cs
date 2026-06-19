using EcoMart.Core.Entities;

namespace EcoMart.Core.Interfaces
{
    /// <summary>
    /// Bản mô tả công việc (hợp đồng) cho lớp truy cập dữ liệu User.
    /// Interface này định nghĩa NHỮNG GÌ có thể làm với dữ liệu User,
    /// mà KHÔNG quan tâm đến việc thực hiện cụ thể như thế nào.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Tìm kiếm một người dùng theo tên đăng nhập.
        /// Dùng khi đăng nhập: lấy thông tin user để so sánh mật khẩu.
        /// Trả về null nếu không tìm thấy.
        /// </summary>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Thêm một người dùng mới vào cơ sở dữ liệu.
        /// Dùng khi đăng ký tài khoản mới.
        /// </summary>
        Task AddAsync(User user);

        /// <summary>
        /// Kiểm tra xem tên đăng nhập đã tồn tại trong hệ thống chưa.
        /// Dùng khi đăng ký: tránh trùng tên đăng nhập.
        /// </summary>
        Task<bool> UsernameExistsAsync(string username);
    }
}
