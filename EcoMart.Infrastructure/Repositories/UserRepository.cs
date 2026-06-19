using EcoMart.Core.Entities;
using EcoMart.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMart.Infrastructure.Repositories
{
    /// <summary>
    /// Người thợ thực sự thực thi hợp đồng IUserRepository.
    /// Chứa code SQL (thông qua Entity Framework Core) để
    /// đọc và ghi dữ liệu User vào SQL Server.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        // _context là "cầu nối" giữa code C# và SQL Server.
        // Thông qua nó, chúng ta viết code C# thay vì viết câu lệnh SQL thủ công.
        private readonly AppDbContext _context;

        // Constructor: khi UserRepository được tạo ra,
        // nó nhận vào AppDbContext từ bên ngoài (Dependency Injection).
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tìm kiếm user trong database theo tên đăng nhập.
        /// Trả về null nếu không tìm thấy.
        /// </summary>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            // _context.Users  → trỏ tới bảng Users trong SQL Server
            // .FirstOrDefaultAsync() → lấy bản ghi đầu tiên khớp điều kiện,
            //                          hoặc trả về null nếu không có
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Thêm một user mới vào database.
        /// </summary>
        public async Task AddAsync(User user)
        {
            // AddAsync → đưa user vào "hàng chờ" để lưu
            await _context.Users.AddAsync(user);

            // SaveChangesAsync → THỰC SỰ ghi vào database (chạy câu INSERT SQL)
            // Nếu không có dòng này, dữ liệu sẽ không được lưu!
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Kiểm tra xem tên đăng nhập đã có trong database chưa.
        /// </summary>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            // AnyAsync → trả về true nếu TỒN TẠI ít nhất 1 bản ghi khớp điều kiện
            //            trả về false nếu không có bản ghi nào
            return await _context.Users
                .AnyAsync(u => u.Username == username);
        }
    }
}
