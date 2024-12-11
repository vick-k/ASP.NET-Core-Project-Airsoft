using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Tests
{
	public static class InMemoryDatabaseSeeder
	{
		public static List<City> Cities;
		public static List<Terrain> Terrains;
		public static List<Team> Teams;
		public static List<ApplicationUser> Users = new List<ApplicationUser>();
		public static List<Game> Games;

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

			dbContext.Terrains.AddRange(Terrains);

			ApplicationUser playerOne = new ApplicationUser()
			{
				UserName = "player1",
				NormalizedUserName = "PLAYER1",
				Email = "player1@gmail.com",
				NormalizedEmail = "PLAYER1@GMAIL.COM",
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true,
				TeamId = Guid.Parse("94b747a1-82fd-4f7c-a9f7-e7b31d1a6756")
			};

			ApplicationUser playerTwo = new ApplicationUser()
			{
				UserName = "player2",
				NormalizedUserName = "PLAYER2",
				Email = "player2@gmail.com",
				NormalizedEmail = "PLAYER2@GMAIL.COM",
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true,
				TeamId = Guid.Parse("97948e5c-0534-4754-86ac-7b09ed8aefef")
			};

			ApplicationUser manager = new ApplicationUser()
			{
				UserName = "manager",
				NormalizedUserName = "MANAGER",
				Email = "manager@gmail.com",
				NormalizedEmail = "MANAGER@GMAIL.COM",
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true
			};

			Users.Add(playerOne);
			Users.Add(playerTwo);
			Users.Add(manager);
			dbContext.Users.AddRange(Users);

			Teams = new List<Team>()
			{
				new Team()
				{
					Id = Guid.Parse("94b747a1-82fd-4f7c-a9f7-e7b31d1a6756"),
					Name = "Team One",
					LogoUrl = "https://example.com",
					CityId = 1,
					LeaderId = Users.First(u => u.UserName == "player1").Id
				},
				new Team()
				{
					Id = Guid.Parse("97948e5c-0534-4754-86ac-7b09ed8aefef"),
					Name = "Team Two",
					LogoUrl = "https://example.com",
					CityId = 2,
					LeaderId = Users.First(u => u.UserName == "player2").Id
				}
			};

			dbContext.Teams.AddRange(Teams);

			Games = new List<Game>()
			{
				new Game()
				{
					Id = Guid.Parse("1e83cba8-74e2-4552-9a3c-0d216016a688"),
					Name = "Test Game #1",
					Description = "Description for Test Game #1.",
					Date = DateTime.Parse("2026-12-01"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					TerrainId = Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e"),
					OrganizerId = Users.First(u => u.UserName == "manager").Id
				},
				new Game()
				{
					Id = Guid.Parse("87536500-af29-4d83-afa8-17ba088f6be8"),
					Name = "Test Game #2",
					Description = "Description for Test Game #2.",
					Date = DateTime.Parse("2024-12-13"),
					StartTime = "11:30",
					Capacity = 120,
					Fee = 0m,
					TerrainId = Guid.Parse("a8177ba1-f5af-4ec4-8631-41a1a5250460"),
					OrganizerId = Users.First(u => u.UserName == "manager").Id
				}
			};

			dbContext.Games.AddRange(Games);

			dbContext.SaveChanges();
		}
	}
}
