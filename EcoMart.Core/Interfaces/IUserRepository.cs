using EcoMart.Core.Entities;

namespace EcoMart.Core.Interfaces
{
    
    public interface IUserRepository
    {
        
        Task<User?> GetByUsernameAsync(string username);

        
        Task AddAsync(User user);

       
        Task<bool> UsernameExistsAsync(string username);
    }
}
