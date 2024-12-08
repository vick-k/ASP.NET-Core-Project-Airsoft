using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProjectAirsoft.Data.Models;

using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Data.Configurations
{
	public static class DatabaseSeeder
	{
		public static void SeedRoles(IServiceProvider serviceProvider)
		{
			RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			string[] roles = { AdminRoleName, ManagerRoleName, UserRoleName };

			foreach (var role in roles)
			{
				bool roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();

				if (!roleExists)
				{
					IdentityResult result = roleManager.CreateAsync(new IdentityRole<Guid> { Name = role }).GetAwaiter().GetResult();

					if (!result.Succeeded)
					{
						throw new Exception($"Failed to create role: {role}");
					}
				}
			}
		}

		public static void AssignAdminRole(IServiceProvider serviceProvider)
		{
			UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			string adminUserName = "admin";
			string adminEmail = "admin@gmail.com";
			string adminPassword = "ASDqwe123";

			ApplicationUser? adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

			if (adminUser == null)
			{
				adminUser = new ApplicationUser()
				{
					UserName = adminUserName,
					Email = adminEmail
				};

				IdentityResult createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();

				if (!createUserResult.Succeeded)
				{
					throw new Exception($"Failed to create admin user: {adminUserName}");
				}
			}

			bool isInRole = userManager.IsInRoleAsync(adminUser, AdminRoleName).GetAwaiter().GetResult();

			if (!isInRole)
			{
				IdentityResult addRoleResult = userManager.AddToRoleAsync(adminUser, AdminRoleName).GetAwaiter().GetResult();

				if (!addRoleResult.Succeeded)
				{
					throw new Exception($"Failed to assign admin role to user: {adminUserName}");
				}
			}
		}

		public static void AssignManagerRole(IServiceProvider serviceProvider)
		{
			UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			string managerUserName = "manager";
			string managerEmail = "manager@gmail.com";

			ApplicationUser? managerUser = userManager.FindByEmailAsync(managerEmail).GetAwaiter().GetResult();

			if (managerUser == null)
			{
				throw new Exception($"The manager with username '{managerEmail}' has not been seeded into the database.");
			}

			bool isInRole = userManager.IsInRoleAsync(managerUser, ManagerRoleName).GetAwaiter().GetResult();

			if (!isInRole)
			{
				IdentityResult addRoleResult = userManager.AddToRoleAsync(managerUser, ManagerRoleName).GetAwaiter().GetResult();

				if (!addRoleResult.Succeeded)
				{
					throw new Exception($"Failed to assign admin role to user: {managerUserName}");
				}
			}
		}
	}
}
