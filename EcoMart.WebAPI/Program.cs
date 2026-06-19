using EcoMart.Core.Interfaces;
using EcoMart.Infrastructure;
using EcoMart.Infrastructure.Repositories;
using EcoMart.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// ĐĂNG KÝ DỊCH VỤ (Dependency Injection)
// Nói với hệ thống: "Khi ai đó cần IAuthService, hãy tạo AuthService"
// ============================================================

// 1. Kết nối database: đọc connection string từ appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký Repository: khi cần IUserRepository → dùng UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 3. Đăng ký Service: khi cần IAuthService → dùng AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// 4. Thêm Controllers để WebAPI nhận HTTP request
builder.Services.AddControllers();

// 5. Swagger: giao diện web để test API (chỉ dùng khi phát triển)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// CẤU HÌNH PIPELINE (Thứ tự xử lý request)
// ============================================================

// Bật Swagger UI để test API qua trình duyệt
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kích hoạt routing đến các Controller
app.MapControllers();

app.Run();
