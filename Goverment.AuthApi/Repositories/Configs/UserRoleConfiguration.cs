using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.DataAccess.EntityConfigurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{

		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable("userroles").HasKey(ck=> new 
			{
				ck.UserId,
				ck.RoleId
			});

			// Column configuration
			builder.Property(uoc => uoc.UserId).HasColumnName("userid").HasColumnType("int");
			builder.Property(uoc => uoc.RoleId).HasColumnName("roleid").HasColumnType("int");

            builder.HasData([new UserRole { UserId=1,RoleId=1}, new UserRole { UserId = 1, RoleId = 2 }]);
		}
	}
}
