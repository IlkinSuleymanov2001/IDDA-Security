namespace Goverment.AuthApi.Commans.Utility.Securities;

public class UserContext
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }

    public UserContext(string userId, List<string> roles)
    {
        UserId = userId;
        Roles = roles;
    }
}
