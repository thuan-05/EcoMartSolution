namespace EcoMart.Core.Exceptions
{
    public class DuplicateUsernameException : Exception
    {
        public DuplicateUsernameException(string username)
            : base($"Tên đăng nhập '{username}' đã tồn tại. Vui lòng chọn tên khác.")
        {
        }
    }
}
