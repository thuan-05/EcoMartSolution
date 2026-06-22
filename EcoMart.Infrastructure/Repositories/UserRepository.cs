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
        /// Tên đăng nhập nằm ở bảng Accounts, nên cần Include Account.
        /// Trả về null nếu không tìm thấy.
        /// </summary>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            // _context.Users      → bảng Users
            // .Include(u => u.Account) → JOIN thêm bảng Accounts để lấy Username & PasswordHash
            // .FirstOrDefaultAsync() → lấy bản ghi đầu tiên khớp điều kiện,
            //                          hoặc trả về null nếu không có
            return await _context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Account != null && u.Account.Username == username);
        }

        /// <summary>
        /// Thêm một user mới vào database.
        /// EF Core tự động lưu cả bảng Users lẫn Accounts trong 1 lần SaveChanges.
        /// </summary>
        public async Task AddAsync(User user)
        {
            // AddAsync → đưa user (kèm Account lồng bên trong) vào "hàng chờ" để lưu
            await _context.Users.AddAsync(user);

            // SaveChangesAsync → THỰC SỰ ghi vào database
            // EF Core tự sinh: INSERT INTO Users ... rồi INSERT INTO Accounts ...
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Kiểm tra xem tên đăng nhập đã có trong database chưa.
        /// Tìm trong bảng Accounts vì Username đã chuyển sang bảng đó.
        /// </summary>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            // AnyAsync → trả về true nếu TỒN TẠI ít nhất 1 bản ghi khớp điều kiện
            //            trả về false nếu không có bản ghi nào
            return await _context.Accounts
                .AnyAsync(a => a.Username == username);
        }
    }
}
