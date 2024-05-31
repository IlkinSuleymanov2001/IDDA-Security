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
            builder.Property(u=>u.IsVerify).IsRequired().HasColumnName("isverify").HasMaxLength(20);
            builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasMaxLength(40);
            builder.Property(u => u.PasswordSalt).IsRequired().HasColumnName("passwordsalt");
            builder.Property(u => u.PasswordHash).IsRequired().HasColumnName("passwordhash");
            builder.Property(u => u.Status).IsRequired().HasColumnName("status");
			builder.Property(u => u.FirstName).HasMaxLength(50).HasColumnName("firstname");
			builder.Property(u => u.LastName).HasMaxLength(50).HasColumnName("lastname");
            builder.Property(u=>u.IsResetPassword).IsRequired().HasColumnName("isresetpassword").HasMaxLength(20);
            builder.Property(u => u.ConfirmToken).HasMaxLength(400).HasColumnName("confirmtoken").HasColumnType("text");
            builder.Property(u => u.OtpCode).HasMaxLength(7).HasColumnName("otpcode");
            builder.Property(u => u.OptCreatedDate).HasMaxLength(50).HasColumnName("otpcreateddate");




            // Configure relationships
            builder.HasMany(u => u.UserRoles)
                   .WithOne(oc => oc.User)
                   .HasForeignKey(oc => oc.UserId);


            //default user 
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("Suleymanov!23", out passwordHash, out passwordSalt);

            builder.HasData([new User
            {  Id = 1,
               FirstName="Ilkin",
               LastName="Suleymanov",
               Email="ilkinsuleymanov200@gmail.com",
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,
                IsVerify = true,
            }]);
        }


    }
}
