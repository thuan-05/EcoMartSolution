using EcoMart.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoMart.Infrastructure
{
    
    public class AppDbContext : DbContext
    {
        
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
                entity.ToTable("Users"); 

                // Id: INT, Khóa chính, tự động tăng (IDENTITY)
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd(); 

                
                entity.Property(u => u.FullName)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired(); 

            
                entity.Property(u => u.Email)
                      .HasColumnType("VARCHAR(100)")
                      .IsRequired(); 
                entity.HasIndex(u => u.Email)
                      .IsUnique(); 

                // PhoneNumber: VARCHAR(15), có thể NULL
                entity.Property(u => u.PhoneNumber)
                      .HasColumnType("VARCHAR(15)")
                      .IsRequired(false); 
            });

            // ════════════════════════════════════════════════════════════════
            // BẢNG Accounts
            // ════════════════════════════════════════════════════════════════
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");

                // AccountId: INT, Khóa chính
                entity.HasKey(a => a.AccountId);
                entity.Property(a => a.AccountId)
                      .ValueGeneratedNever(); 

                entity.Property(a => a.Username)
                      .HasColumnType("VARCHAR(50)")
                      .IsRequired();
                entity.HasIndex(a => a.Username)
                      .IsUnique();

                entity.Property(a => a.PasswordHash)
                      .HasColumnType("VARCHAR(MAX)")
                      .IsRequired();

                entity.Property(a => a.Role)
                      .HasColumnType("VARCHAR(20)")
                      .IsRequired();

                // IsActive: BIT, NOT NULL, mặc định = 1 (true)
                entity.Property(a => a.IsActive)
                      .HasColumnType("BIT")
                      .HasDefaultValue(true);

                // ── Quan hệ 1-1: Account ←→ User 
                entity.HasOne(a => a.User)          
                      .WithOne(u => u.Account)     
                      .HasForeignKey<Account>(a => a.AccountId); 
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
                      .ValueGeneratedOnAdd(); 

                entity.Property(a => a.UserId)
                      .IsRequired();

                // ReceiverName: NVARCHAR(100), NOT NULL
                entity.Property(a => a.ReceiverName)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

            
                entity.Property(a => a.ReceiverPhone)
                      .HasColumnType("VARCHAR(15)")
                      .IsRequired();

                
                entity.Property(a => a.SpecificAddress)
                      .HasColumnType("NVARCHAR(255)")
                      .IsRequired();

            
                entity.Property(a => a.Ward)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

            
                entity.Property(a => a.District)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                
                entity.Property(a => a.Province)
                      .HasColumnType("NVARCHAR(100)")
                      .IsRequired();

                // IsDefault: BIT, NOT NULL, mặc định = 0 (false)
                entity.Property(a => a.IsDefault)
                      .HasColumnType("BIT")
                      .HasDefaultValue(false);

                // ── Quan hệ 1-Nhiều: User có Nhiều Address 
                entity.HasOne(a => a.User)           
                      .WithMany(u => u.Addresses)       
                      .HasForeignKey(a => a.UserId)      
                      .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}

