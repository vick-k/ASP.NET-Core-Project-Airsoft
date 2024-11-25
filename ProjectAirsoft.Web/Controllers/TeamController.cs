using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Team;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class TeamController(IBaseService baseService, ITeamService teamService, ICityService cityService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			IEnumerable<TeamIndexViewModel> allTeams = await teamService.GetAllTeamsAsync();

			return View(allTeams);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			IEnumerable<CityListModel> cities = await cityService.GetAllCitiesForListAsync();

			TeamFormModel viewModel = new TeamFormModel()
			{
				Cities = cities
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TeamFormModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			string userId = userManager.GetUserId(User)!;
			bool result = await teamService.AddTeamAsync(viewModel, userId);

			if (result == false)
			{
				// add generic error message
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			TeamDetailsViewModel viewModel = await teamService.GetTeamDetailsAsync(teamGuid);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Join(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.GetUserAsync(User);

			if (user!.TeamId != null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			TeamJoinViewModel viewModel = await teamService.GetTeamJoinAsync(teamGuid);

			if (viewModel == null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Join(TeamJoinViewModel viewModel)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.GetUserAsync(User);

			if (user!.TeamId != null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.JoinTeamAsync(teamGuid, user);

			if (result == false)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			// add success message
			return RedirectToAction(nameof(Details), new { id = viewModel.Id });
		}

		[HttpGet]
		public async Task<IActionResult> Leave(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.GetUserAsync(User);

			if (user!.TeamId == null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			TeamLeaveViewModel viewModel = await teamService.GetTeamLeaveAsync(teamGuid);

			if (viewModel == null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			if (viewModel.Id != user.TeamId.ToString() || viewModel.Leader == user.UserName)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Leave(TeamLeaveViewModel viewModel)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.GetUserAsync(User);

			if (user!.TeamId == null)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.LeaveTeamAsync(teamGuid, user);

			if (result == false)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string userId = userManager.GetUserId(User)!;

			TeamFormModel viewModel = await teamService.GetTeamForEditAsync(id, userId);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			IEnumerable<CityListModel> cities = await cityService.GetAllCitiesForListAsync();
			viewModel.Cities = cities;

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(TeamFormModel viewModel, string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			if (!ModelState.IsValid)
			{
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			bool teamExists = await teamService.TeamExistsAsync(id);

			if (teamExists == false)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await teamService.EditTeamAsync(viewModel, teamGuid);

			if (isEdited == false)
			{
				// add generic error message
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			// add success message
			return RedirectToAction(nameof(Details), new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			TeamDeleteViewModel viewModel = await teamService.GetTeamForDeleteAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			ApplicationUser? user = await userManager.GetUserAsync(User);

			if (user!.UserName != viewModel.Leader)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(TeamDeleteViewModel viewModel)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.DeleteTeamAsync(teamGuid);

			if (result == false)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}
	}
}
