namespace Core.Security.Entities;

public class UserLoginSecurity
{
    public int UserId { get; set; }
    public int LoginRetryCount { get; set; }
    public bool IsAccountBlock { get; set; }
    public DateTime? AccountBlockedTime { get; set; }
    public DateTime? AccountUnblockedTime { get; set; }
    public virtual User User { get; set; }

}
