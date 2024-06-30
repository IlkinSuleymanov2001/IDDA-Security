using Goverment.Core.Persistance.Repositories;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.Entities.Audit;

namespace Core.Security.Entities;
public  class User : Entity, IAuditEntity
{
    public string FullName  { get; set; }
	public string Email { get; set; }
    public string? OtpCode { get; set; }
    public string? IDToken { get; set; }
    public DateTime? OptCreatedDate { get; set; }
    public DateTime? IDTokenExpireDate { get; set; }
    public bool IsVerify { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
   

    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual UserLoginSecurity UserLoginSecurity { get; set; }
    public virtual UserResendOtpSecurity UserResendOtpSecurity { get; set; }

    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

}

