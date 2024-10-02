using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Back_End.Infrastructure.Context.Configurations
{
	public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "USER"
				},
				new IdentityRole
				{
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				}
			);
		}
	}
}
