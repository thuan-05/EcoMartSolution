using EcoMart.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoMart.Infrastructure
{
    /// <summary>
    /// AppDbContext là "cầu nối" chính thức giữa code C# và SQL Server.
    /// Entity Framework Core dùng class này để biết:
    /// - Kết nối tới database nào
    /// - Bảng nào trong database tương ứng với class nào trong code
    /// </summary>
    public class AppDbContext : DbContext
    {
        // Constructor nhận vào các tùy chọn cấu hình (connection string, v.v.)
        // và chuyển cho lớp cha DbContext xử lý.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet<User> = đại diện cho bảng Users trong SQL Server.
        /// Khi viết _context.Users → EF Core hiểu là "bảng Users".
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
