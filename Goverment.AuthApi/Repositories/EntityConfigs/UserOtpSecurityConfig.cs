using Core.Security.Entities;
using Goverment.Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.Repositories.EntityConfigs
{
    public class UserOtpSecurityConfig : IEntityTypeConfiguration<UserOtpSecurity>
    {
        public void Configure(EntityTypeBuilder<UserOtpSecurity> builder)
        {
            builder.ToTable("UserOtpSecurities").HasKey(us => us.UserId);
            builder.Property(us => us.UserId).HasColumnName("userid").HasColumnType("int");
            builder.Property(us => us.TryOtpCount).HasColumnName("tryotpcount").HasMaxLength(2);

            builder.Property(us => us.unBlockDate).HasColumnName("unblockdate");
            builder.Property(us => us.IsLock).HasColumnName("islock").HasMaxLength(5);
            builder.HasOne(us => us.User)
                .WithOne(u => u.UserOtpSecurity)
                .HasForeignKey<UserOtpSecurity>(u => u.UserId);

            builder.HasData([new UserOtpSecurity { UserId = 1}]);
        }
    }
}
