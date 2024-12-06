using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;

using static ProjectAirsoft.Common.AlertMessages.User;
using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserManagementController(IBaseService baseService, IUserService userService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<UserIndexViewModel> users = await userService.GetAllUsersAsync();

			return View(users);
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

			bool userExists = await userService.UserExistsAsync(userId);

			if (userExists == false)
			{
				TempData[AlertDanger] = UserDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			bool result = await userService.AssignUserToRoleAsync(userId, role);

			if (result == false)
			{
				TempData[AlertDanger] = UserAssignRoleFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = UserAssignRoleSuccessMessage;

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

			bool userExists = await userService.UserExistsAsync(userId);

			if (userExists == false)
			{
				TempData[AlertDanger] = UserDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			if (string.IsNullOrEmpty(role))
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await userService.RemoveUserRoleAsync(userId, role);

			if (result == false)
			{
				TempData[AlertDanger] = UserRemoveRoleFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = UserRemoveRoleSuccessMessage;

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

			bool userExists = await userService.UserExistsAsync(userId);

			if (userExists == false)
			{
				TempData[AlertDanger] = UserDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			string adminUsername = User.Identity!.Name!;

			ApplicationUser? admin = await userService.GetUserAsync(adminUsername);

			if (admin != null)
			{
				if (admin.Id.ToString() == userId)
				{
					TempData[AlertDanger] = AdminDeleteHimselfErrorMessage;

					return RedirectToAction(nameof(Index));
				}
			}

			bool result = await userService.DeleteUserAsync(userId);

			if (result == false)
			{
				TempData[AlertDanger] = UserDeleteFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = UserDeleteSuccessMessage;

			return RedirectToAction(nameof(Index));
		}
	}
}
