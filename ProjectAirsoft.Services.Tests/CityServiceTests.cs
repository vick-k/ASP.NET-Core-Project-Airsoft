using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Services.Tests.InMemoryDatabaseSeeder;

namespace ProjectAirsoft.Services.Tests
{
	public class CityServiceTests
	{
		private DbContextOptions<ApplicationDbContext> dbOptions;
		private ApplicationDbContext dbContext;

		private ICityService cityService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase("ApplicationDbInMemory" + Guid.NewGuid().ToString())
				.Options;

			dbContext = new ApplicationDbContext(dbOptions, false);

			dbContext.Database.EnsureCreated();
			SeedDatabase(dbContext);

			cityService = new CityService(dbContext);
		}

		[OneTimeTearDown]
		public void TearDownBase()
		{
			dbContext.Dispose();
		}

		[Test]
		public async Task GetAllCitiesForListAsyncShouldReturnAllCities()
		{
			var allCities = await cityService.GetAllCitiesForListAsync();

			Assert.That(allCities.Any(c => c.Name == "Sofia"));
			Assert.That(allCities.Any(c => c.Name == "Plovdiv"));
			Assert.That(allCities.Any(c => c.Name == "Varna"));
		}

		[TestCase(1, 2, 3)]
		public async Task GetCityForEditAsyncShouldReturnRequestedCity(int a, int b, int c)
		{
			var citySofia = await cityService.GetCityForEditAsync(a);
			var cityPlovdiv = await cityService.GetCityForEditAsync(b);
			var cityVarna = await cityService.GetCityForEditAsync(c);

			Assert.That(citySofia.Name, Is.EqualTo("Sofia"));
			Assert.That(cityPlovdiv.Name, Is.EqualTo("Plovdiv"));
			Assert.That(cityVarna.Name, Is.EqualTo("Varna"));
		}

		[Test]
		public async Task GetCityForEditAsyncShouldReturnNullWhenCityDoesNotExists()
		{
			var cityNull = await cityService.GetCityForEditAsync(999);

			Assert.That(cityNull, Is.EqualTo(null));
		}

		[Test]
		public async Task EditCityAsyncShouldReturnTrueWhenCityExists()
		{
			City cityForEdit = Cities.First(c => c.Id == 4);

			CityFormModel cityFormModel = new CityFormModel()
			{
				Name = "Shumen"
			};

			bool result = await cityService.EditCityAsync(cityFormModel, cityForEdit.Id);

			Assert.That(result, Is.True);
			Assert.That(cityForEdit.Name, Is.EqualTo(cityFormModel.Name));
		}

		[Test]
		public async Task EditCityAsyncShouldReturnFalseWhenCityDoesNotExists()
		{
			City? cityForEdit = Cities.FirstOrDefault(c => c.Id == 999);

			CityFormModel cityFormModel = new CityFormModel()
			{
				Name = "Shumen"
			};

			bool result = await cityService.EditCityAsync(cityFormModel, 999);

			Assert.That(result, Is.False);
		}

		[TestCase(1, 2, 3)]
		public async Task CityExistsAsyncShouldReturnTrueIfCityExists(int a, int b, int c)
		{
			bool resultA = await cityService.CityExistsAsync(a);
			bool resultB = await cityService.CityExistsAsync(b);
			bool resultC = await cityService.CityExistsAsync(c);

			Assert.That(resultA, Is.True);
			Assert.That(resultB, Is.True);
			Assert.That(resultC, Is.True);
		}

		[Test]
		public async Task CityExistsAsyncShouldReturnFalseIfCityDoesNotExists()
		{
			bool result = await cityService.CityExistsAsync(999);

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task AddCityAsyncShouldReturnTrueIfInputIsValid()
		{
			CityInputViewModel cities = new CityInputViewModel()
			{
				CityNames = "Ruse\r\nPleven"
			};

			bool result = await cityService.AddCityAsync(cities);

			Assert.That(result, Is.True);
			Assert.That(dbContext.Cities.Any(c => c.Name == "Ruse"));
			Assert.That(dbContext.Cities.Any(c => c.Name == "Pleven"));
		}

		[Test]
		public async Task AddCityAsyncShouldReturnFalseIfInputIsInvalid()
		{
			CityInputViewModel city = new CityInputViewModel()
			{
				CityNames = "Sofia"
			};

			bool result = await cityService.AddCityAsync(city);

			Assert.That(result, Is.False);
		}
	}
}
