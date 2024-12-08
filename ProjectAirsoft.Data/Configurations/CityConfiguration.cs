using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.HasData(GenerateCities());
		}

		private IEnumerable<City> GenerateCities()
		{
			IEnumerable<City> cities = new List<City>()
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
				},
				new City()
				{
					Id = 5,
					Name = "Ruse"
				},
				new City()
				{
					Id = 6,
					Name = "Stara Zagora"
				},
				new City()
				{
					Id = 7,
					Name = "Pleven"
				},
				new City()
				{
					Id = 8,
					Name = "Sliven"
				},
				new City()
				{
					Id = 9,
					Name = "Dobrich"
				},
				new City()
				{
					Id = 10,
					Name = "Shumen"
				}
			};

			return cities;
		}
	}
}
