namespace Goverment.Core.Security.JWT
{
    public  class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }

    public class PermissionTokens:Tokens
    {
        public string[] Permissons { get; set; }
    }
}
