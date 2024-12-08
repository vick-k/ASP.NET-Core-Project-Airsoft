using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasData(GenerateUsers());
		}

		private IEnumerable<ApplicationUser> GenerateUsers()
		{
			ApplicationUser player1 = new ApplicationUser()
			{
				Id = Guid.Parse("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"),
				UserName = "player1",
				NormalizedUserName = "PLAYER1",
				Email = "player1@gmail.com",
				NormalizedEmail = "PLAYER1@GMAIL.COM",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true
			};

			ApplicationUser player2 = new ApplicationUser()
			{
				Id = Guid.Parse("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"),
				UserName = "player2",
				NormalizedUserName = "PLAYER2",
				Email = "player2@gmail.com",
				NormalizedEmail = "PLAYER2@GMAIL.COM",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true
			};

			ApplicationUser manager = new ApplicationUser()
			{
				Id = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336"),
				UserName = "manager",
				NormalizedUserName = "MANAGER",
				Email = "manager@gmail.com",
				NormalizedEmail = "MANAGER@GMAIL.COM",
				SecurityStamp = Guid.NewGuid().ToString(),
				LockoutEnabled = true
			};

			PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

			player1.PasswordHash = passwordHasher.HashPassword(player1, "ASDqwe123");
			player2.PasswordHash = passwordHasher.HashPassword(player2, "ASDqwe123");
			manager.PasswordHash = passwordHasher.HashPassword(manager, "ASDqwe123");

			IEnumerable<ApplicationUser> users = new List<ApplicationUser>()
			{
				player1,
				player2,
				manager
			};

			return users;
		}
	}
}
