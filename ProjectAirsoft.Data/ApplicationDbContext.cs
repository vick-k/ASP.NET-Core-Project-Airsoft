using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Configurations;
using ProjectAirsoft.Data.Models;
using System.Reflection;

namespace ProjectAirsoft.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		private readonly bool seedDb;

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool seedDb = true)
			: base(options)
		{
			this.seedDb = seedDb;
		}

		public virtual DbSet<Game> Games { get; set; } = null!;

		public virtual DbSet<Terrain> Terrains { get; set; } = null!;

		public virtual DbSet<City> Cities { get; set; } = null!;

		public virtual DbSet<UserGame> UsersGames { get; set; } = null!;

		public virtual DbSet<Comment> Comments { get; set; } = null!;

		public virtual DbSet<Team> Teams { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new UserGameConfiguration());

			if (seedDb)
			{
				builder.ApplyConfiguration(new ApplicationUserConfiguration());
				builder.ApplyConfiguration(new CityConfiguration());
				builder.ApplyConfiguration(new GameConfiguration());
				builder.ApplyConfiguration(new TerrainConfiguration());
			}

			base.OnModelCreating(builder);
		}
	}
}
