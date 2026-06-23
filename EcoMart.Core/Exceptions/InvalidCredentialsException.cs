namespace EcoMart.Core.Exceptions
{
   
    public class InvalidCredentialsException : Exception
    {
        
        public InvalidCredentialsException()
            : base("Tên đăng nhập hoặc mật khẩu không đúng.")
        {
        }
    }
}
