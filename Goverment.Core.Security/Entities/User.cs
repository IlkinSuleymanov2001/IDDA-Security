using Core.Security.Enums;
using Goverment.Core.Persistance.Repositories;

namespace Core.Security.Entities;

public  class User : Entity
{
    public string FirstName  { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
    public string? ConfirmToken { get; set; }
    public string? OtpCode { get; set; }
    public bool IsResetPassword { get; set; }
    public DateTime? OptCreatedDate { get; set; }
    public bool IsVerify { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
	public virtual ICollection<UserRole> UserRoles { get; set; }

    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

    public User(int id,string email, byte[] passwordSalt, byte[] passwordHash,
                bool status,bool isVerify) : this()
    {
        Id = id;
		Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
        IsVerify = isVerify;
       
    }

}

