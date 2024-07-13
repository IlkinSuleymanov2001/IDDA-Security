using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users").HasKey(u => u.Id);
            // Configure properties
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.OtpCode).IsUnique().HasFilter("\"otpcode\" IS NOT NULL");
            builder.HasIndex(u => u.IDToken).IsUnique().HasFilter("\"idtoken\" IS NOT NULL");

            builder.Property(u=>u.IsVerify).IsRequired().HasColumnName("isverify").HasMaxLength(20);
            builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasMaxLength(40);
            builder.Property(u => u.PasswordHash).IsRequired().HasColumnName("passwordhash");
            builder.Property(u => u.Status).IsRequired().HasColumnName("status");
			builder.Property(u => u.FullName).HasMaxLength(50).HasColumnName("firstname");
            builder.Property(u => u.OtpCode).HasMaxLength(7).HasColumnName("otpcode");
            builder.Property(u => u.OptCreatedDate).HasMaxLength(50).HasColumnName("otpcreateddate");
            builder.Property(u => u.IDToken).HasMaxLength(200).HasColumnName("idtoken");
            builder.Property(u => u.IDTokenExpireDate).HasMaxLength(50).HasColumnName("idtokenexpiredate");
            builder.Property(u => u.CreatedTime).HasMaxLength(50).HasColumnName("createdtime");
            builder.Property(u => u.ModifiedTime).HasMaxLength(50).HasColumnName("modifiedtime");

            


            // Configure relationships
            builder.HasMany(u => u.UserRoles)
                   .WithOne(oc => oc.User)
                   .HasForeignKey(oc => oc.UserId);


            //default user 
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("Suleymanov!23", out passwordHash, out passwordSalt);

            builder.HasData([new User
            {  Id = 1,
               FullName="Ilkin  Suleymanov",
               Email="ilkinsuleymanov200@gmail.com",
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,
                IsVerify = true,
            }]);
        }


    }
}
