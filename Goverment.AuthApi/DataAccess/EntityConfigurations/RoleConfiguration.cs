using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goverment.AuthApi.DataAccess.EntityConfigurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("roles").HasKey(x => x.Id);

			builder.Property(x=>x.Name).IsRequired().
				HasColumnName("name").HasMaxLength(100);

			builder.HasMany(x => x.UserRoles).WithOne(x => x.Role)
				.HasForeignKey(x => x.RoleId);
            builder.HasData([new Role { Id = 2, Name = "USER" },new Role { Id = 1, Name = "ADMIN" }]);
		}
	}
}
