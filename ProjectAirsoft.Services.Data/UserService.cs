using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class UserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager) : BaseService, IUserService
	{
		public async Task<IEnumerable<UserIndexViewModel>> GetAllUsersAsync()
		{
			List<ApplicationUser> users = await userManager.Users
				.Where(u => u.IsDeleted == false)
				.ToListAsync();
			List<UserIndexViewModel> userViewModels = new List<UserIndexViewModel>();

			foreach (ApplicationUser user in users)
			{
				IList<string> roles = await userManager.GetRolesAsync(user);

				userViewModels.Add(new UserIndexViewModel()
				{
					Id = user.Id.ToString(),
					Username = user.UserName!,
					Email = user.Email!,
					Roles = roles.ToList()
				});
			}

			return userViewModels;
		}

		public async Task<bool> UserExistsAsync(string id)
		{
			ApplicationUser? user = await userManager
				.FindByIdAsync(id);

			if (user == null)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> AssignUserToRoleAsync(string userId, string role)
		{
			ApplicationUser? user = await userManager.FindByIdAsync(userId);
			bool roleExists = await roleManager.RoleExistsAsync(role);

			if (user == null || user.IsDeleted || roleExists == false)
			{
				return false;
			}

			bool alreadyInRole = await userManager.IsInRoleAsync(user, role);

			if (alreadyInRole == false)
			{
				IdentityResult result = await userManager.AddToRoleAsync(user, role);

				if (result.Succeeded == false)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> RemoveUserRoleAsync(string userId, string role)
		{
			ApplicationUser? user = await userManager.FindByIdAsync(userId);
			bool roleExists = await roleManager.RoleExistsAsync(role);

			if (user == null || user.IsDeleted || roleExists == false)
			{
				return false;
			}

			bool alreadyInRole = await userManager.IsInRoleAsync(user, role);

			if (alreadyInRole)
			{
				IdentityResult result = await userManager.RemoveFromRoleAsync(user, role);

				if (result.Succeeded == false)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> DeleteUserAsync(string id)
		{
			ApplicationUser? user = await userManager.FindByIdAsync(id);

			if (user == null || user.IsDeleted)
			{
				return false;
			}

			await dbContext
					.Entry(user)
					.Collection(u => u.UsersGames)
					.LoadAsync();

			user.UsersGames.Clear();
			user.IsDeleted = true;

			await userManager.UpdateAsync(user);

			return true;
		}

		public async Task<ApplicationUser?> GetUserAsync(string username)
		{
			return await userManager.FindByNameAsync(username);
		}
	}
}
