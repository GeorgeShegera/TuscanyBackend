namespace Tuscany.Utility
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Login(string username, string password)
        {
            UserName = username;
            Password = password;
        }

    }
}
