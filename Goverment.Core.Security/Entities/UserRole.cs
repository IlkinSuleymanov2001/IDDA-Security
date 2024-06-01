using Goverment.Core.Persistance.Repositories;

namespace Core.Security.Entities;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }


    public UserRole()
    {
    }
    public UserRole(int userId, int roleId) : base()
    {
        UserId = userId;
        RoleId = roleId;
    }
}