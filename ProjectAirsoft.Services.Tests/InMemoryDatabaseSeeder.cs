using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Tests
{
	public static class InMemoryDatabaseSeeder
	{
		public static List<City> Cities;

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

			dbContext.Cities.AddRange(Cities);

			dbContext.SaveChanges();
		}
	}
}
