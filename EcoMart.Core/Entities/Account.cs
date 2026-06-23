namespace EcoMart.Core.Entities
{
   
    public class Account
    {
       
        public int AccountId { get; set; }

      
        public string Username { get; set; } = string.Empty;

       
        public string PasswordHash { get; set; } = string.Empty;

        
        public string Role { get; set; } = string.Empty;

       
        public bool IsActive { get; set; } = true;

     

        public User User { get; set; } = null!;
    }
}
