using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Tests
{
	public static class InMemoryDatabaseSeeder
	{
		public static List<City> Cities;
		public static List<Terrain> Terrains;

		public static void SeedDatabase(ApplicationDbContext dbContext)
		{
			Cities = new List<City>()
			{
				new City()
				{
					Id = 1,
					Name = "Sofia"
				},
				new City()
				{
					Id = 2,
					Name = "Plovdiv"
				},
				new City()
				{
					Id = 3,
					Name = "Varna"
				},
				new City()
				{
					Id = 4,
					Name = "Burgas"
				}
			};

			Terrains = new List<Terrain>()
			{
				new Terrain()
				{
					Id = Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e"),
					Name = "Residence",
					LocationUrl = "https://maps.app.goo.gl/epw8PCoW2TsQamtF7",
					CityId = 2
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

			dbContext.Cities.AddRange(Cities);
			dbContext.Terrains.AddRange(Terrains);

			dbContext.SaveChanges();
		}
	}
}
