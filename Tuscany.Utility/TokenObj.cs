namespace Tuscany.Utility
{
    public class TokenObj
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }
        public TokenObj(string token, string username, DateTime expires)
        {
            Token = token;
            UserName = username;
            Expires = expires;
        }
    }
}
