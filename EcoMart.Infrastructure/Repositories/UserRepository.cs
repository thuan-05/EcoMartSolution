using EcoMart.Core.Entities;
using EcoMart.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMart.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Account != null && u.Account.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Accounts
                .AnyAsync(a => a.Username == username);
        }
    }
}
