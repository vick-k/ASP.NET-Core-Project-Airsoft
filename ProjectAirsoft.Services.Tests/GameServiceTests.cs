using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Services.Tests.InMemoryDatabaseSeeder;

namespace ProjectAirsoft.Services.Tests
{
	public class GameServiceTests
	{
		private DbContextOptions<ApplicationDbContext> dbOptions;
		private ApplicationDbContext dbContext;
		private IGameService gameService;

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

			gameService = new GameService(dbContext);
		}

		[TearDown]
		public void TearDownBase()
		{
			dbContext.Dispose();
		}

		[Test]
		public async Task GetAllGamesAsyncWithoutFilterShouldReturnAllGames()
		{
			AllGamesFilterViewModel viewModel = new AllGamesFilterViewModel();

			var allGames = await gameService.GetAllGamesAsync(viewModel);

			Assert.That(allGames.Count, Is.EqualTo(Games.Count));
			Assert.That(allGames.Any(g => g.Name == "Test Game #1"));
			Assert.That(allGames.Any(g => g.Name == "Test Game #2"));
		}

		[Test]
		public async Task GetAllGamesAsyncWithValidFilterShouldReturnTheFilteredGames()
		{
			AllGamesFilterViewModel viewModel = new AllGamesFilterViewModel();
			viewModel.TerrainFilter = "Residence";

			var filteredGames = await gameService.GetAllGamesAsync(viewModel);
			int filteredGamesCount = Games.Where(g => g.TerrainId == Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e")).Count();

			Assert.That(filteredGames.Count, Is.EqualTo(filteredGamesCount));
			Assert.That(filteredGames.Any(g => g.Name == "Test Game #1"));
		}

		[Test]
		public async Task GetAllGamesAsyncWithValidFilterButNoGamesShouldReturnNoGames()
		{
			AllGamesFilterViewModel viewModel = new AllGamesFilterViewModel();
			viewModel.TerrainFilter = "Not existing terrain";

			var filteredGames = await gameService.GetAllGamesAsync(viewModel);

			Assert.That(filteredGames.Count, Is.EqualTo(0));
		}

		[Test]
		public async Task AddGameAsyncShouldReturnTrueIfInputIsValid()
		{
			ApplicationUser gameCreator = Users.First(u => u.UserName == "manager");

			GameFormModel game = new GameFormModel()
			{
				Name = "Test Game #999",
				Description = "Description for Test Game #999",
				Date = "2024-12-20",
				StartTime = "10:00",
				Capacity = 100,
				Fee = 0,
				OrganizerId = Users.First(u => u.UserName == "manager").Id.ToString(),
				TerrainId = "c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"
			};

			bool result = await gameService.AddGameAsync(game, gameCreator.Id.ToString());

			Assert.That(result, Is.True);
			Assert.That(dbContext.Games.Any(g => g.Name == game.Name), Is.True);
		}

		[Test]
		public async Task AddGameAsyncShouldReturnFalseIfDateIsInvalid()
		{
			ApplicationUser gameCreator = Users.First(u => u.UserName == "manager");

			GameFormModel game = new GameFormModel()
			{
				Name = "Test Game #999",
				Description = "Description for Test Game #999",
				Date = "2024-13-20",
				StartTime = "10:00",
				Capacity = 100,
				Fee = 0,
				OrganizerId = Users.First(u => u.UserName == "manager").Id.ToString(),
				TerrainId = "c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"
			};

			bool result = await gameService.AddGameAsync(game, gameCreator.Id.ToString());

			Assert.That(result, Is.False);
			Assert.That(dbContext.Games.Any(g => g.Name == game.Name), Is.False);
		}

		[Test]
		public async Task GetGameDetailsAsyncWithoutUserShouldReturnTheRequestedGame()
		{
			Game game = Games.First(g => g.Name == "Test Game #2");

			var requestedGame = await gameService.GetGameDetailsAsync(game.Id, null);

			Assert.That(requestedGame, Is.Not.Null);
			Assert.That(requestedGame.Id, Is.EqualTo(game.Id.ToString()));
		}

		[Test]
		public async Task GetGameDetailsAsyncWithUserShouldReturnTheRequestedGame()
		{
			ApplicationUser user = Users.First(u => u.UserName == "player1");
			Game game = Games.First(g => g.Name == "Test Game #1");

			var requestedGame = await gameService.GetGameDetailsAsync(game.Id, user.Id.ToString());

			Assert.That(requestedGame, Is.Not.Null);
			Assert.That(requestedGame.Id, Is.EqualTo(game.Id.ToString()));
		}

		[Test]
		public async Task GetGameDetailsAsyncShouldReturnNullIfGameDoesNotExists()
		{
			var requestedGame = await gameService.GetGameDetailsAsync(Guid.Parse("16fa24d9-8c47-4b1c-af7c-348bc666846b"), null);

			Assert.That(requestedGame, Is.Null);
		}

		[Test]
		public async Task GetGameForEditAsyncShouldReturnTheRequestedGame()
		{
			var requestedGame = await gameService.GetGameForEditAsync("1e83cba8-74e2-4552-9a3c-0d216016a688");

			Assert.That(requestedGame, Is.Not.Null);
			Assert.That(requestedGame.Name, Is.EqualTo("Test Game #1"));
		}

		[Test]
		public async Task GetGameForEditAsyncShouldReturnNullIfGameDoesNotExists()
		{
			var requestedGame = await gameService.GetGameForEditAsync("16fa24d9-8c47-4b1c-af7c-348bc666846b");

			Assert.That(requestedGame, Is.Null);
		}

		[Test]
		public async Task EditGameAsyncShouldReturnTrueIfInputIsValid()
		{
			Game gameForEdit = Games.First(g => g.Name == "Test Game #1");

			GameFormModel model = new GameFormModel()
			{
				Name = "Test Game #999",
				Description = "Description for Test Game #999",
				Date = "2024-12-20",
				StartTime = "10:00",
				Capacity = 100,
				Fee = 0,
				OrganizerId = Users.First(u => u.UserName == "manager").Id.ToString(),
				TerrainId = "c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"
			};

			bool result = await gameService.EditGameAsync(model, gameForEdit.Id);

			Assert.That(result, Is.True);
			Assert.That(gameForEdit.Name, Is.EqualTo(model.Name));
			Assert.That(gameForEdit.Description, Is.EqualTo(model.Description));
			Assert.That(gameForEdit.Date, Is.EqualTo(DateTime.Parse(model.Date)));
			Assert.That(gameForEdit.Capacity, Is.EqualTo(model.Capacity));
			Assert.That(gameForEdit.Fee, Is.EqualTo(model.Fee));
		}

		[Test]
		public async Task EditGameAsyncShouldReturnFalseIfDateIsInvalid()
		{
			Game gameForEdit = Games.First(g => g.Name == "Test Game #1");

			GameFormModel model = new GameFormModel()
			{
				Name = "Test Game #999",
				Description = "Description for Test Game #999",
				Date = "2024-12-35",
				StartTime = "10:00",
				Capacity = 100,
				Fee = 0,
				OrganizerId = Users.First(u => u.UserName == "manager").Id.ToString(),
				TerrainId = "c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"
			};

			bool result = await gameService.EditGameAsync(model, gameForEdit.Id);

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task EditGameAsyncShouldReturnFalseIfGameDoesNotExists()
		{
			GameFormModel model = new GameFormModel()
			{
				Name = "Test Game #999",
				Description = "Description for Test Game #999",
				Date = "2024-12-20",
				StartTime = "10:00",
				Capacity = 100,
				Fee = 0,
				OrganizerId = Users.First(u => u.UserName == "manager").Id.ToString(),
				TerrainId = "c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"
			};

			bool result = await gameService.EditGameAsync(model, Guid.Parse("16fa24d9-8c47-4b1c-af7c-348bc666846b"));

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetGameForCancelAsyncShouldReturnTheRequestedGame()
		{
			Game game = Games.First(g => g.Name == "Test Game #1");

			var requestedGame = await gameService.GetGameForCancelAsync(game.Id.ToString());

			Assert.That(requestedGame, Is.Not.Null);
			Assert.That(requestedGame.Id, Is.EqualTo(game.Id.ToString()));
		}

		[Test]
		public async Task GetGameForCancelAsyncShouldReturnNullIfGameDoesNotExists()
		{
			var requestedGame = await gameService.GetGameForCancelAsync("16fa24d9-8c47-4b1c-af7c-348bc666846b");

			Assert.That(requestedGame, Is.Null);
		}

		[Test]
		public async Task CancelGameAsyncShouldReturnTrueInPositiveCases()
		{
			Game game = Games.First(g => g.Name == "Test Game #1");
			ApplicationUser user = Users.First(u => u.UserName == "manager");

			bool result = await gameService.CancelGameAsync(game.Id, user.Id.ToString());

			Assert.That(result, Is.True);
			Assert.That(game.IsCanceled, Is.True);
		}

		[Test]
		public async Task CancelGameAsyncShouldReturnFalseIfGameDoesNotExists()
		{
			ApplicationUser user = Users.First(u => u.UserName == "manager");

			bool result = await gameService.CancelGameAsync(Guid.Parse("16fa24d9-8c47-4b1c-af7c-348bc666846b"), user.Id.ToString());

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GameExistsAsyncShouldReturnTrueIfGameExists()
		{
			bool result = await gameService.GameExistsAsync("1e83cba8-74e2-4552-9a3c-0d216016a688");

			Assert.That(result, Is.True);
		}

		[Test]
		public async Task GameExistsAsyncShouldReturnFalseIfGameDoesNotExists()
		{
			var result = await gameService.GameExistsAsync("16fa24d9-8c47-4b1c-af7c-348bc666846b");

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetGameCapacityAsyncShouldReturnTheCapacityOfTheGame()
		{
			Game game = Games.First(g => g.Name == "Test Game #1");

			int capacity = await gameService.GetGameCapacityAsync(game.Id.ToString());

			Assert.That(capacity, Is.EqualTo(game.Capacity));
		}

		[Test]
		public async Task GetGameForDeleteAsyncShouldReturnTheRequestedGame()
		{
			Game game = Games.First(g => g.Name == "Test Game #2");

			var requestedGame = await gameService.GetGameForDeleteAsync(game.Id.ToString());

			Assert.That(requestedGame, Is.Not.Null);
			Assert.That(requestedGame.Id, Is.EqualTo(game.Id.ToString()));
		}

		[Test]
		public async Task GetGameForDeleteAsyncShouldReturnNullIfGameDoesNotExists()
		{
			var requestedGame = await gameService.GetGameForDeleteAsync("16fa24d9-8c47-4b1c-af7c-348bc666846b");

			Assert.That(requestedGame, Is.Null);
		}

		[Test]
		public async Task DeleteGameAsyncShouldReturnTrueInPositiveCases()
		{
			Game game = Games.First(g => g.Name == "Test Game #2");

			bool result = await gameService.DeleteGameAsync(game.Id);

			Assert.That(result, Is.True);
			Assert.That(game.IsDeleted, Is.True);
		}

		[Test]
		public async Task DeleteGameAsyncShouldReturnFalseIfGameDoesNotExists()
		{
			bool result = await gameService.DeleteGameAsync(Guid.Parse("16fa24d9-8c47-4b1c-af7c-348bc666846b"));

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetAllCommentsAsyncShouldReturnAllCommentsForAGame()
		{
			Game game = Games.First(g => g.Name == "Test Game #1");
			ApplicationUser user = Users.First(u => u.UserName == "player1");

			var allComments = await gameService.GetAllCommentsAsync(game.Id);

			Assert.That(allComments.Count(), Is.EqualTo(game.Comments.Count));
		}

		[Test]
		public async Task GetAllGamesForAdminAreaAsyncShouldReturnAllGames()
		{
			var allGames = await gameService.GetAllGamesForAdminAreaAsync();

			Assert.That(allGames, Is.Not.Null);
			Assert.That(allGames.Count(), Is.EqualTo(Games.Count));
		}
	}
}
