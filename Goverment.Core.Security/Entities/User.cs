using Goverment.Core.Persistance.Repositories;

namespace Core.Security.Entities;

public  class User : Entity
{
    public string FirstName  { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
    public string? ConfirmToken { get; set; }
    public string? OtpCode { get; set; }
    public DateTime? OptCreatedDate { get; set; }
    public bool IsVerify { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }

    //public string? ImageUrl { get; set; }
	public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual UserLoginSecurity UserLoginSecurity { get; set; }

    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

}

