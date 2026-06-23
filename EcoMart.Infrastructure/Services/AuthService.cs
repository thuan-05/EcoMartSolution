using EcoMart.Core.DTOs;
using EcoMart.Core.Entities;
using EcoMart.Core.Enums;
using EcoMart.Core.Exceptions;
using EcoMart.Core.Interfaces;

namespace EcoMart.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            bool exists = await _userRepository.UsernameExistsAsync(dto.Username);
            if (exists)
                throw new DuplicateUsernameException(dto.Username);

            // KHÔNG lưu mật khẩu thô — chỉ lưu hash
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                FullName    = dto.FullName,
                Email       = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Account = new Account
                {
                    Username     = dto.Username,
                    PasswordHash = passwordHash,
                    Role         = UserRole.Customer.ToString(),
                    IsActive     = true
                }
            };

            await _userRepository.AddAsync(newUser);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null)
                throw new InvalidCredentialsException();

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.Account!.PasswordHash);
            if (!isPasswordCorrect)
                throw new InvalidCredentialsException(); // Cùng exception để không lộ username đúng/sai

            return user.FullName;
        }
    }
}
