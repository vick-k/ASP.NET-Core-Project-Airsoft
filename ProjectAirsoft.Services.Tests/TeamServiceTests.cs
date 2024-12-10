using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Team;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Services.Tests.InMemoryDatabaseSeeder;

namespace ProjectAirsoft.Services.Tests
{
	public class TeamServiceTests
	{
		private DbContextOptions<ApplicationDbContext> dbOptions;
		private ApplicationDbContext dbContext;
		private ITeamService teamService;

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

			teamService = new TeamService(dbContext);
		}

		[TearDown]
		public void TearDownBase()
		{
			dbContext.Dispose();
		}

		[Test]
		public async Task GetAllTeamsAsyncWithoutFilterShouldReturnAllTeams()
		{
			var allTeams = await teamService.GetAllTeamsAsync();

			Assert.That(allTeams.Count, Is.EqualTo(Teams.Count));
			Assert.That(allTeams.Any(t => t.Name == "Team One"));
			Assert.That(allTeams.Any(t => t.Name == "Team Two"));
		}

		[Test]
		public async Task GetAllTeamsAsyncWithValidFilterShouldReturnFilteredTeams()
		{
			var filteredTeams = await teamService.GetAllTeamsAsync("Sofia");

			Assert.That(filteredTeams.Any(t => t.Name == "Team One"));
			Assert.That(filteredTeams.Any(t => t.Name == "Team Two"), Is.False);
		}

		[Test]
		public async Task GetAllTeamsAsyncWithValidFilterButNoTeamsShouldReturnNoTeams()
		{
			var noTeams = await teamService.GetAllTeamsAsync("Pernik");

			Assert.That(noTeams.Count, Is.EqualTo(0));
		}

		[Test]
		public async Task AddTeamAsyncShouldReturnTrueIfInputIsValid()
		{
			ApplicationUser teamCreator = Users.First(u => u.UserName == "manager");

			TeamFormModel team = new TeamFormModel()
			{
				Name = "Test Team",
				LogoUrl = "https://example.com",
				CityId = 1,
				LeaderId = teamCreator.Id.ToString()
			};

			bool result = await teamService.AddTeamAsync(team, teamCreator.Id.ToString());

			Assert.That(result, Is.True);
			Assert.That(teamCreator.TeamId, Is.Not.Null);
			Assert.That(dbContext.Teams.Any(t => t.Name == team.Name));
		}

		[Test]
		public async Task AddTeamAsyncShouldReturnFalseIfUserIdIsNotValid()
		{
			TeamFormModel team = new TeamFormModel()
			{
				Name = "Test Team",
				LogoUrl = "https://example.com",
				CityId = 1,
				LeaderId = "invalid-user-id"
			};

			bool result = await teamService.AddTeamAsync(team, "invalid-user-id");

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task AddTeamAsyncShouldReturnFalseIfUserIsAlreadyInTeam()
		{
			ApplicationUser teamCreator = Users.First(u => u.UserName == "player1");

			TeamFormModel team = new TeamFormModel()
			{
				Name = "Test Team",
				LogoUrl = "https://example.com",
				CityId = 1,
				LeaderId = teamCreator.Id.ToString()
			};

			bool result = await teamService.AddTeamAsync(team, teamCreator.Id.ToString());

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTeamDetailsAsyncShouldReturnTheRequestedTeam()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			var requestedTeam = await teamService.GetTeamDetailsAsync(team.Id);

			Assert.That(requestedTeam, Is.Not.Null);
			Assert.That(requestedTeam.Id, Is.EqualTo(team.Id.ToString()));
		}

		[Test]
		public async Task GetTeamDetailsAsyncShouldReturnNullIfTeamDoesNotExists()
		{
			var requestedTeam = await teamService.GetTeamDetailsAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"));

			Assert.That(requestedTeam, Is.Null);
		}

		[Test]
		public async Task GetTeamJoinAsyncShouldReturnTheRequestedTeam()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			var requestedTeam = await teamService.GetTeamJoinAsync(team.Id);

			Assert.That(requestedTeam, Is.Not.Null);
			Assert.That(requestedTeam.Id, Is.EqualTo(team.Id.ToString()));
			Assert.That(requestedTeam.Name, Is.EqualTo(team.Name));
			Assert.That(requestedTeam.City, Is.EqualTo(team.City.Name));
		}

		[Test]
		public async Task GetTeamJoinAsyncShouldReturnNullIfTeamDoesNotExists()
		{
			var requestedTeam = await teamService.GetTeamJoinAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"));

			Assert.That(requestedTeam, Is.Null);
		}

		[Test]
		public async Task JoinTeamAsyncShouldReturnTrueInPositiveCases()
		{
			Team team = Teams.First(t => t.Name == "Team One");
			ApplicationUser joiningUser = Users.First(u => u.UserName == "manager");

			bool result = await teamService.JoinTeamAsync(team.Id, joiningUser);

			Assert.That(result, Is.True);
			Assert.That(joiningUser.TeamId, Is.EqualTo(team.Id));
			Assert.That(team.Members.Any(m => m.UserName == joiningUser.UserName), Is.True);
		}

		[Test]
		public async Task JoinTeamAsyncShouldReturnFalseIfTeamDoesNotExists()
		{
			ApplicationUser joiningUser = Users.First(u => u.UserName == "manager");

			bool result = await teamService.JoinTeamAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"), joiningUser);

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTeamLeaveAsyncShouldReturnTheRequestedTeam()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			var requestedTeam = await teamService.GetTeamLeaveAsync(team.Id);

			Assert.That(requestedTeam, Is.Not.Null);
			Assert.That(requestedTeam.Id, Is.EqualTo(team.Id.ToString()));
			Assert.That(requestedTeam.Name, Is.EqualTo(team.Name));
			Assert.That(requestedTeam.City, Is.EqualTo(team.City.Name));
		}

		[Test]
		public async Task GetTeamLeaveAsyncShouldReturnNullIfTeamDoesNotExists()
		{
			var requestedTeam = await teamService.GetTeamLeaveAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"));

			Assert.That(requestedTeam, Is.Null);
		}

		[Test]
		public async Task LeaveTeamAsyncShouldReturnTrueInPositiveCases()
		{
			Team team = Teams.First(t => t.Name == "Team One");
			ApplicationUser user = Users.First(u => u.UserName == "manager");
			await teamService.JoinTeamAsync(team.Id, user);

			bool result = await teamService.LeaveTeamAsync(team.Id, user);

			Assert.That(result, Is.True);
			Assert.That(user.TeamId, Is.Null);
			Assert.That(team.Members.Any(m => m.UserName == user.UserName), Is.False);
		}

		[Test]
		public async Task LeaveTeamAsyncShouldReturnFalseIfTeamDoesNotExists()
		{
			ApplicationUser user = Users.First(u => u.UserName == "manager");

			bool result = await teamService.LeaveTeamAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"), user);

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task TeamExistsAsyncShouldReturnTrueIfTeamExists()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			bool result = await teamService.TeamExistsAsync(team.Id.ToString());
		}

		[Test]
		public async Task TeamExistsAsyncShouldReturnFalseIfTeamDoesNotExist()
		{
			bool result = await teamService.TeamExistsAsync("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d");

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTeamForEditAsyncShouldReturnTheRequestedTeam()
		{
			Team team = Teams.First(t => t.Name == "Team One");
			ApplicationUser teamLeader = Users.First(u => u.UserName == "player1");

			var requestedTeam = await teamService.GetTeamForEditAsync(team.Id.ToString(), teamLeader.Id.ToString());

			Assert.That(requestedTeam.Id, Is.EqualTo(team.Id.ToString()));
			Assert.That(requestedTeam.Name, Is.EqualTo(team.Name));
			Assert.That(requestedTeam.LogoUrl, Is.EqualTo(team.LogoUrl));
			Assert.That(requestedTeam.CityId, Is.EqualTo(team.CityId));
			Assert.That(requestedTeam.LeaderId, Is.EqualTo(team.LeaderId.ToString()));
		}

		[Test]
		public async Task GetTeamForEditAsyncShouldReturnNullIfTeamDoesNotExist()
		{
			ApplicationUser user = Users.First(u => u.UserName == "player1");

			var requestedTeam = await teamService.GetTeamForEditAsync("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d", user.Id.ToString());

			Assert.That(requestedTeam, Is.Null);
		}

		[Test]
		public async Task GetTeamForEditAsyncShouldReturnNullIfUserIsNotLeader()
		{
			Team team = Teams.First(t => t.Name == "Team One");
			ApplicationUser user = Users.First(u => u.UserName == "player2");

			var requestedTeam = await teamService.GetTeamForEditAsync(team.Id.ToString(), user.Id.ToString());

			Assert.That(requestedTeam, Is.Null);
		}

		[Test]
		public async Task EditTeamAsyncShouldReturnTrueIfInputIsValid()
		{
			Team teamForEdit = Teams.First(t => t.Name == "Team One");

			TeamFormModel model = new TeamFormModel()
			{
				Name = "Team One v2.0",
				LogoUrl = "https://example.com",
				CityId = 2
			};

			bool result = await teamService.EditTeamAsync(model, teamForEdit.Id);

			Assert.That(result, Is.True);
			Assert.That(teamForEdit.Name, Is.EqualTo(model.Name));
			Assert.That(teamForEdit.LogoUrl, Is.EqualTo(model.LogoUrl));
			Assert.That(teamForEdit.CityId, Is.EqualTo(model.CityId));
		}

		[Test]
		public async Task EditTeamAsyncShouldReturnFalseIfTeamDoesNotExists()
		{
			TeamFormModel model = new TeamFormModel()
			{
				Name = "Team One v2.0",
				LogoUrl = "https://example.com",
				CityId = 2
			};

			bool result = await teamService.EditTeamAsync(model, Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"));

			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetTeamForDeleteAsyncShouldReturnTheRequestedTeam()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			var teamForDelete = await teamService.GetTeamForDeleteAsync(team.Id.ToString());

			Assert.That(teamForDelete.Id, Is.EqualTo(team.Id.ToString()));
		}

		[Test]
		public async Task GetTeamForDeleteAsyncShouldReturnNullIfTeamDoesNotExists()
		{
			var teamForDelete = await teamService.GetTeamForDeleteAsync("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d");

			Assert.That(teamForDelete, Is.Null);
		}

		[Test]
		public async Task DeleteTeamAsyncShouldReturnTrueInPositiveCases()
		{
			Team team = Teams.First(t => t.Name == "Team One");

			bool result = await teamService.DeleteTeamAsync(team.Id);

			Assert.That(result, Is.True);
			Assert.That(team.IsDeleted, Is.True);
			foreach (var member in team.Members)
			{
				Assert.That(member.TeamId, Is.Null);
			}
		}

		[Test]
		public async Task DeleteTeamAsyncShouldReturnFalseIfTeamDoesNotExists()
		{
			bool result = await teamService.DeleteTeamAsync(Guid.Parse("c472fc1f-55d0-4230-98ce-2bf6ac8ed35d"));

			Assert.That(result, Is.False);
		}
	}
}
