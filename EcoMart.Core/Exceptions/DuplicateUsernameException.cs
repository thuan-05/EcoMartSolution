namespace EcoMart.Core.Exceptions
{
    
    public class DuplicateUsernameException : Exception
    {
        
        /// <param name="username">Tên đăng nhập bị trùng lặp.</param>
        public DuplicateUsernameException(string username)
            : base($"Tên đăng nhập '{username}' đã tồn tại. Vui lòng chọn tên khác.")
        {
        }
    }
}
