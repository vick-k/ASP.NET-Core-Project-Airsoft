using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Terrain;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Services.Tests.InMemoryDatabaseSeeder;

namespace ProjectAirsoft.Services.Tests
{
	public class TerrainServiceTests
	{
		private DbContextOptions<ApplicationDbContext> dbOptions;
		private ApplicationDbContext dbContext;
		private ITerrainService terrainService;

		[SetUp]
		public void SetUp()
		{
			dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase("ApplicationDbInMemory" + Guid.NewGuid().ToString())
				.Options;

			dbContext = new ApplicationDbContext(dbOptions, false);
			dbContext.Database.EnsureDeleted();
			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);

			terrainService = new TerrainService(dbContext);
		}

		[TearDown]
		public void TearDownBase()
		{
			dbContext.Dispose();
		}

		[Test]
		public async Task GetAllTerrainsForListAsyncShouldReturnAllTerrains()
		{
			var allTerrains = await terrainService.GetAllTerrainsForListAsync();

			Assert.That(allTerrains.Count, Is.EqualTo(Terrains.Count));
			Assert.That(allTerrains.Any(t => t.Name == "Residence"));
			Assert.That(allTerrains.Any(t => t.Name == "Airsoft Sofia Field"));
			Assert.That(allTerrains.Any(t => t.Name == "BoinoPole"));
		}

		[Test]
		public async Task AddTerrainAsyncShouldReturnTrueIfInputIsValid()
		{
			TerrainFormModel terrain = new TerrainFormModel()
			{
				Name = "Test Terrain",
				LocationUrl = "https://example.com",
				CityId = 3
			};

			bool result = await terrainService.AddTerrainAsync(terrain);

			Assert.That(result, Is.True);
			Assert.That(dbContext.Terrains.Any(t => t.Name == "Test Terrain"));
		}

		[Test]
		public async Task AddTerrainAsyncShouldReturnFalseIfInputIsInvalid()
		{
			TerrainFormModel terrain = new TerrainFormModel()
			{
				Name = "Residence",
				LocationUrl = "https://example.com",
				CityId = 2
			};

			bool result = await terrainService.AddTerrainAsync(terrain);

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTerrainForEditAsyncShouldReturnTheRequestedTerrain()
		{
			var terrain = await terrainService.GetTerrainForEditAsync("838d3b35-4413-4721-870e-32b00bde5f8e");

			Assert.That(terrain.Name == "Residence");
		}

		[Test]
		public async Task GetTerrainForEditAsyncShouldReturnNullIfTerrainDoesNotExists()
		{
			var terrain = await terrainService.GetTerrainForEditAsync("63b8246a-aa5c-4964-95c9-10d42eb07b65");

			Assert.That(terrain, Is.EqualTo(null));
		}

		[Test]
		public async Task TerrainExistsAsyncShouldReturnTrueIfTerrainExists()
		{
			bool result = await terrainService.TerrainExistsAsync("a8177ba1-f5af-4ec4-8631-41a1a5250460");

			Assert.That(result, Is.True);
		}

		[Test]
		public async Task TerrainExistsAsyncShouldReturnFalseIfTerrainDoesNotExists()
		{
			bool result = await terrainService.TerrainExistsAsync("63b8246a-aa5c-4964-95c9-10d42eb07b65");

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task EditTerrainAsyncShouldReturnTrueIfTerrainExists()
		{
			Terrain terrainForEdit = Terrains.First(t => t.Id == Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e"));

			TerrainFormModel terrainFormModel = new TerrainFormModel()
			{
				Name = "Residence v2.0",
				LocationUrl = "https://example.com",
				CityId = 2
			};

			bool result = await terrainService.EditTerrainAsync(terrainFormModel, terrainForEdit.Id);

			Assert.That(result, Is.True);
			Assert.That(terrainForEdit.Name, Is.EqualTo(terrainFormModel.Name));
		}

		[Test]
		public async Task EditTerrainAsyncShouldReturnFalseIfTerrainDoesNotExists()
		{
			Terrain? terrainForEdit = Terrains.FirstOrDefault(t => t.Id == Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"));

			TerrainFormModel terrainFormModel = new TerrainFormModel()
			{
				Name = "Residence v2.0",
				LocationUrl = "https://example.com",
				CityId = 2
			};

			bool result = await terrainService.EditTerrainAsync(terrainFormModel, Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"));

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTerrainForDeleteAsyncShouldReturnTheRequestedTerrain()
		{
			var terrain = await terrainService.GetTerrainForDeleteAsync("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a");

			Assert.That(terrain.Name, Is.EqualTo("BoinoPole"));
		}

		[Test]
		public async Task GetTerrainForDeleteAsyncShouldReturnNullIfTerrainDoesNotExists()
		{
			var terrain = await terrainService.GetTerrainForDeleteAsync("63b8246a-aa5c-4964-95c9-10d42eb07b65");

			Assert.That(terrain, Is.Null);
		}

		[Test]
		public async Task DeleteTerrainAsyncShouldReturnTrueIfTerrainExists()
		{
			Terrain terrainForDelete = Terrains.First(t => t.Name == "Residence");

			bool result = await terrainService.DeleteTerrainAsync(terrainForDelete.Id);

			Assert.That(result, Is.True);
			Assert.That(terrainForDelete.IsDeleted, Is.True);
			Assert.That(terrainForDelete.IsActive, Is.False);
		}

		[Test]
		public async Task DeleteTerrainAsyncShouldReturnFalseIfTerrainDoesNotExists()
		{
			bool result = await terrainService.DeleteTerrainAsync(Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"));

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task TerrainStatusChangeAsyncShouldReturnTrueIfTerrainExists()
		{
			Terrain terrainForStatusChange = Terrains.First(t => t.Name == "Residence");

			bool result = await terrainService.TerrainStatusChangeAsync(terrainForStatusChange.Id);

			Assert.That(result, Is.True);
			Assert.That(terrainForStatusChange.IsActive, Is.False);
		}

		[Test]
		public async Task TerrainStatusChangeAsyncShouldReturnFalseIfTerrainDoesNotExists()
		{
			bool result = await terrainService.TerrainStatusChangeAsync(Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"));

			Assert.That(result, Is.False);
		}
	}
}
