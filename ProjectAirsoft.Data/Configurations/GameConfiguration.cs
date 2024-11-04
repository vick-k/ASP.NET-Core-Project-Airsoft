using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class GameConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
			builder
				.Property(t => t.Fee)
				.HasColumnType("decimal(18,2)");
		}
	}
}
