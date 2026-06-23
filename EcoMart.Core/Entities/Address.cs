namespace EcoMart.Core.Entities
{
    
    public class Address
    {
      
        public int Id { get; set; }

       
        public int UserId { get; set; }

        
        public string ReceiverName { get; set; } = string.Empty;

       
        public string ReceiverPhone { get; set; } = string.Empty;

        
        public string SpecificAddress { get; set; } = string.Empty;

      
        public string Ward { get; set; } = string.Empty;

      
        public string District { get; set; } = string.Empty;

      
        public string Province { get; set; } = string.Empty;

      
        public bool IsDefault { get; set; } = false;

       
        public User User { get; set; } = null!;
    }
}
