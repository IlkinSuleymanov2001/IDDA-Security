using Goverment.Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Repositories.EntityConfigs
{
    public class UserOtpSecurityConfig : IEntityTypeConfiguration<UserOtpSecurity>
    {
        public void Configure(EntityTypeBuilder<UserOtpSecurity> builder)
        {
            builder.ToTable("UserOtpSecurities").HasKey(us => us.UserId);
            builder.Property(us => us.UserId).HasColumnName("userid").HasColumnType("int");
            builder.Property(us => us.TryOtpCount).HasColumnName("tryotpcount").HasMaxLength(2);

           /* builder.HasOne(us => us.User)
                .WithOne(u => u.UserOtpSecurity)
                .HasForeignKey<UserOtpSecurity>(u => u.UserId);*/

          //builder.HasData([new UserOtpSecurity { UserId = 1 }]);
        }
    }
}
