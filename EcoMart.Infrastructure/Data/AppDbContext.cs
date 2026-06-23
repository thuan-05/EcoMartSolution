using EcoMart.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoMart.Infrastructure
{
    
    public class AppDbContext : DbContext
    {
        // Constructor nhận vào các tùy chọn cấu hình (connection string, v.v.)
        // và chuyển cho lớp cha DbContext xử lý.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Address> Addresses { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ════════════════════════════════════════════════════════════════
            // BẢNG Users
            // ════════════════════════════════════════════════════════════════
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Tên bảng trong SQL Server

                // Id: INT, Khóa chính, tự động tăng (IDENTITY)
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd(); // → IDENTITY(1,1) trong SQL

                // FullName: NVARCHAR(100), NOT NULL
                // NVARCHAR = Unicode → lưu được tiếng Việt
                entity.Property(u => u.FullName)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired(); // NOT NULL

                // Email: VARCHAR(100), NOT NULL, UNIQUE
                // VARCHAR = không Unicode → đủ cho email (không dấu)
                entity.Property(u => u.Email)
                      .HasColumnType("VARCHAR(100)")
                      .IsRequired(); // NOT NULL
                entity.HasIndex(u => u.Email)
                      .IsUnique(); // UNIQUE CONSTRAINT

                // PhoneNumber: VARCHAR(15), có thể NULL
                entity.Property(u => u.PhoneNumber)
                      .HasColumnType("VARCHAR(15)")
                      .IsRequired(false); // NULL cho phép
            });

            // ════════════════════════════════════════════════════════════════
            // BẢNG Accounts
            // ════════════════════════════════════════════════════════════════
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");

                // AccountId: INT, Khóa chính
                // KHÔNG tự động tăng vì AccountId = Users.Id (khóa ngoại 1-1)
                entity.HasKey(a => a.AccountId);
                entity.Property(a => a.AccountId)
                      .ValueGeneratedNever(); // Không tự tăng, lấy từ Users.Id

                // Username: VARCHAR(50), NOT NULL, UNIQUE
                entity.Property(a => a.Username)
                      .HasColumnType("VARCHAR(50)")
                      .IsRequired();
                entity.HasIndex(a => a.Username)
                      .IsUnique();

                // PasswordHash: VARCHAR(MAX), NOT NULL
                entity.Property(a => a.PasswordHash)
                      .HasColumnType("VARCHAR(MAX)")
                      .IsRequired();

                // Role: VARCHAR(20), NOT NULL
                entity.Property(a => a.Role)
                      .HasColumnType("VARCHAR(20)")
                      .IsRequired();

                // IsActive: BIT, NOT NULL, mặc định = 1 (true)
                entity.Property(a => a.IsActive)
                      .HasColumnType("BIT")
                      .HasDefaultValue(true);

                // ── Quan hệ 1-1: Account ←→ User ─────────────────────────
                // AccountId vừa là Khóa chính, vừa là Khóa ngoại → Users.Id
                entity.HasOne(a => a.User)          // Account có 1 User
                      .WithOne(u => u.Account)       // User có 1 Account
                      .HasForeignKey<Account>(a => a.AccountId); // FK: Accounts.AccountId → Users.Id
            });

            // ════════════════════════════════════════════════════════════════
            // BẢNG Addresses
            // ════════════════════════════════════════════════════════════════
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Addresses");

                // Id: INT, Khóa chính, tự động tăng
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)

                // UserId: INT, NOT NULL, Khóa ngoại → Users.Id
                entity.Property(a => a.UserId)
                      .IsRequired();

                // ReceiverName: NVARCHAR(100), NOT NULL (tiếng Việt)
                entity.Property(a => a.ReceiverName)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                // ReceiverPhone: VARCHAR(15), NOT NULL
                entity.Property(a => a.ReceiverPhone)
                      .HasColumnType("VARCHAR(15)")
                      .IsRequired();

                // SpecificAddress: NVARCHAR(255), NOT NULL
                entity.Property(a => a.SpecificAddress)
                      .HasColumnType("NVARCHAR(255)")
                      .IsRequired();

                // Ward: NVARCHAR(100), NOT NULL
                entity.Property(a => a.Ward)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                // District: NVARCHAR(100), NOT NULL
                entity.Property(a => a.District)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                // Province: NVARCHAR(100), NOT NULL
                entity.Property(a => a.Province)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                // IsDefault: BIT, NOT NULL, mặc định = 0 (false)
                entity.Property(a => a.IsDefault)
                      .HasColumnType("BIT")
                      .HasDefaultValue(false);

                // ── Quan hệ 1-Nhiều: User có Nhiều Address ───────────────
                entity.HasOne(a => a.User)              // Address có 1 User
                      .WithMany(u => u.Addresses)        // User có nhiều Addresses
                      .HasForeignKey(a => a.UserId)      // FK: Addresses.UserId → Users.Id
                      .OnDelete(DeleteBehavior.Cascade); // Xóa User → tự xóa Addresses
            });
        }
    }
}

