namespace ToDoList.Application.Extensions
{
    public class IsVerifyPasswordExtension
    {
        public static bool IsVerifyPassword(string hashPassword, string userPassword)
        {
            var hash = HashPasswordExtension.HashPassword(userPassword);

            return hash == hashPassword;
        }
    }
}
