using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.Web.Areas.Admin.ViewModels;
using System.Data;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserManagementController(IBaseService baseService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<ApplicationUser> users = await userManager.Users
				.Where(u => u.IsDeleted == false)
				.ToListAsync();
			List<UserViewModel> userViewModels = new List<UserViewModel>();

			foreach (ApplicationUser user in users)
			{
				IList<string> roles = await userManager.GetRolesAsync(user);

				userViewModels.Add(new UserViewModel()
				{
					Id = user.Id.ToString(),
					Username = user.UserName!,
					Email = user.Email!,
					Roles = roles.ToList()
				});
			}

			return View(userViewModels);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.FindByIdAsync(userId);
			bool roleExists = await roleManager.RoleExistsAsync(role);

			if (user != null && roleExists)
			{
				await userManager.AddToRoleAsync(user, role);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.FindByIdAsync(userId);
			bool roleExists = await roleManager.RoleExistsAsync(role);

			if (user != null && roleExists)
			{
				await userManager.RemoveFromRoleAsync(user, role);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			Guid userGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.FindByIdAsync(userId);

			if (user != null)
			{
				user.IsDeleted = true;
				await userManager.UpdateAsync(user);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
