using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.DataAccess.EntityConfigurations
{
    public class UserLoginSecurityConfiguration : IEntityTypeConfiguration<UserLoginSecurity>
    {
        public void Configure(EntityTypeBuilder<UserLoginSecurity> builder)
        {
            builder.ToTable("UserLoginSecurities").HasKey(us => us.UserId);
            builder.Property(us => us.UserId).HasColumnName("userid").HasColumnType("int");
            builder.Property(us => us.LoginRetryCount).HasColumnName("LoginRetryCount").HasMaxLength(2);
                
            builder.Property(us => us.AccountBlockedTime).HasColumnName("AccountBlockedTime");
            builder.Property(us => us.IsAccountBlock).HasColumnName("isBlock").HasMaxLength(5);

            builder.Property(us => us.AccountUnblockedTime).HasColumnName("AccountUnBlockedTime");
            builder.HasOne(us => us.User)
                .WithOne(u => u.UserLoginSecurity)
                .HasForeignKey<UserLoginSecurity>(u => u.UserId);

            builder.HasData([new UserLoginSecurity { UserId=1,LoginRetryCount=0,IsAccountBlock=false}]);
        }
    }
}
