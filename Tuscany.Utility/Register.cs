namespace Tuscany.Utility
{
    public class Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Register(string username, string password, string email)
        {
            UserName = username;
            Password = password;
            Email = email;
        }
    }
}
