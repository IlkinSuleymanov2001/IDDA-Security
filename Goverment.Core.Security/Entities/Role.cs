using Goverment.Core.Persistance.Repositories;
using Goverment.Core.Security.JWT;

namespace Core.Security.Entities;

public class Role : Entity
{
    public string Name { get; set; }
	public virtual ICollection<UserRole> UserRoles { get; set; }

	public Role()
    {
		UserRoles = new HashSet<UserRole>();
	}

    public Role(int id, string name) : base(id)
    {
        Name = name;
    }
}