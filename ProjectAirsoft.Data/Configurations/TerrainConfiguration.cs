using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class TerrainConfiguration : IEntityTypeConfiguration<Terrain>
	{
		public void Configure(EntityTypeBuilder<Terrain> builder)
		{
			builder
				.Property(t => t.IsActive)
				.HasDefaultValue(true);
		}
	}
}
