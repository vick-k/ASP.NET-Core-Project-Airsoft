using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
	{
		public void Configure(EntityTypeBuilder<UserGame> builder)
		{
			builder
				.HasKey(ug => new { ug.UserId, ug.GameId });
		}
	}
}
