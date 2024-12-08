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

			builder.HasData(GenerateTerrains());
		}

		private IEnumerable<Terrain> GenerateTerrains()
		{
			IEnumerable<Terrain> terrains = new List<Terrain>()
			{
				new Terrain()
				{
					Id = Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"),
					Name = "Antifreeze",
					LocationUrl = "https://maps.app.goo.gl/5ZFey2vRCUqduQZ48",
					CityId = 3
				},
				new Terrain()
				{
					Id = Guid.Parse("8b031c23-6687-4338-9845-5e9af3af951c"),
					Name = "Horse Base",
					LocationUrl = "https://maps.app.goo.gl/f3MciMAqqMgjKzNd9",
					CityId = 3
				},
				new Terrain()
				{
					Id = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					Name = "Green Water",
					LocationUrl = "https://maps.app.goo.gl/Uyhob7UD8pahXxXy5",
					CityId = 10
				},
				new Terrain()
				{
					Id = Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e"),
					Name = "Residence",
					LocationUrl = "https://maps.app.goo.gl/epw8PCoW2TsQamtF7",
					CityId = 10
				},
				new Terrain()
				{
					Id = Guid.Parse("a8177ba1-f5af-4ec4-8631-41a1a5250460"),
					Name = "Airsoft Sofia Field",
					LocationUrl = "https://maps.app.goo.gl/Q8E4aBXsvyYjyzRj6",
					CityId = 1
				},
				new Terrain()
				{
					Id = Guid.Parse("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"),
					Name = "BoinoPole",
					LocationUrl = "https://maps.app.goo.gl/qzZ24mimwva19fff7",
					CityId = 1
				}
			};

			return terrains;
		}
	}
}
