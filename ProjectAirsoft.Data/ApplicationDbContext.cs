using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using System.Reflection;

namespace ProjectAirsoft.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Game> Games { get; set; } = null!;

		public virtual DbSet<Terrain> Terrains { get; set; } = null!;

		public virtual DbSet<City> Cities { get; set; } = null!;

		public virtual DbSet<UserGame> UsersGames { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
