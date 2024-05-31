using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.DataAccess.EntityConfigurations
{
	public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserRole>
	{

		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable("userroles").Ignore(uoc => uoc.Id).HasKey(ck=> new 
			{
				ck.UserId,
				ck.RoleId
			});

			// Column configuration
			builder.Property(uoc => uoc.UserId).HasColumnName("userid").HasColumnType("int");
			builder.Property(uoc => uoc.RoleId).HasColumnName("roleid").HasColumnType("int");

            builder.HasData([new UserRole { UserId=1,RoleId=1}]);
		}
	}
}
