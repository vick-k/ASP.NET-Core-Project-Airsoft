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

			Teams = new List<Team>()
			{
				new Team()
				{
					Id = Guid.Parse("94b747a1-82fd-4f7c-a9f7-e7b31d1a6756"),
					Name = "Team One",
					LogoUrl = "https://example.com",
					CityId = 1,
					LeaderId = Users.First(u => u.UserName == "player1").Id
					// add members?
				},
				new Team()
				{
					Id = Guid.Parse("97948e5c-0534-4754-86ac-7b09ed8aefef"),
					Name = "Team Two",
					LogoUrl = "https://example.com",
					CityId = 2,
					LeaderId = Users.First(u => u.UserName == "player2").Id
					// add members?
				}
			};

			dbContext.Cities.AddRange(Cities);
			dbContext.Terrains.AddRange(Terrains);
			dbContext.Users.AddRange(Users);
			dbContext.Teams.AddRange(Teams);

			dbContext.SaveChanges();
		}
	}
}
