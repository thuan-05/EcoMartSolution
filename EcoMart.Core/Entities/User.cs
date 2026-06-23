namespace EcoMart.Core.Entities
{
    
    public class User
    {
        
        public int Id { get; set; }

        
        public string FullName { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;

      
        public string? PhoneNumber { get; set; }

        public Account? Account { get; set; }

       
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
