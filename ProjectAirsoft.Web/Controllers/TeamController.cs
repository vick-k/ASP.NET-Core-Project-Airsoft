using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Team;

using static ProjectAirsoft.Common.AlertMessages.Team;
using static ProjectAirsoft.Common.ApplicationConstants;

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
				TempData[AlertDanger] = AddTeamFailMessage;
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();

				return View(viewModel);
			}

			TempData[AlertSuccess] = AddTeamSuccessMessage;

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
				TempData[AlertDanger] = JoinTeamAlreadyInTeamMessage;

				return RedirectToAction(nameof(Index));
			}

			TeamJoinViewModel viewModel = await teamService.GetTeamJoinAsync(teamGuid);

			if (viewModel == null)
			{
				TempData[AlertDanger] = TeamGenericErrorMessage;

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
				TempData[AlertDanger] = JoinTeamAlreadyInTeamMessage;

				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.JoinTeamAsync(teamGuid, user);

			if (result == false)
			{
				TempData[AlertDanger] = JoinTeamFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = JoinTeamSuccessMessage;

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
				TempData[AlertDanger] = LeaveTeamNotInTeamMessage;

				return RedirectToAction(nameof(Index));
			}

			TeamLeaveViewModel viewModel = await teamService.GetTeamLeaveAsync(teamGuid);

			if (viewModel == null)
			{
				TempData[AlertDanger] = LeaveTeamFailMessage;

				return RedirectToAction(nameof(Index));
			}

			if (viewModel.Id != user.TeamId.ToString() || viewModel.Leader == user.UserName)
			{
				TempData[AlertDanger] = LeaveTeamFailMessage;

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
				TempData[AlertDanger] = LeaveTeamNotInTeamMessage;

				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.LeaveTeamAsync(teamGuid, user);

			if (result == false)
			{
				TempData[AlertDanger] = LeaveTeamFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = LeaveTeamSuccessMessage;

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
				TempData[AlertDanger] = TeamGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			IEnumerable<CityListModel> cities = await cityService.GetAllCitiesForListAsync();
			viewModel.Cities = cities;

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(TeamFormModel viewModel, string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				TempData[AlertDanger] = EditTeamFailMessage;

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
				TempData[AlertDanger] = TeamDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			string userId = userManager.GetUserId(User)!;

			TeamFormModel teamFromEditForm = await teamService.GetTeamForEditAsync(id, userId);

			if (teamFromEditForm == null || teamFromEditForm.LeaderId != userId)
			{
				TempData[AlertDanger] = EditTeamNotOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await teamService.EditTeamAsync(viewModel, teamGuid);

			if (isEdited == false)
			{
				TempData[AlertDanger] = EditTeamFailMessage;
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();

				return View(viewModel);
			}

			TempData[AlertSuccess] = EditTeamSuccessMessage;

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

			ApplicationUser? user = await userManager.GetUserAsync(User);
			TeamDeleteViewModel teamFromDeleteForm = await teamService.GetTeamForDeleteAsync(viewModel.Id);

			if (user!.UserName != teamFromDeleteForm.Leader)
			{
				TempData[AlertDanger] = DeleteTeamNotOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.DeleteTeamAsync(teamGuid);

			if (result == false)
			{
				TempData[AlertDanger] = DeleteTeamFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = DeleteTeamSuccessMessage;

			return RedirectToAction(nameof(Index));
		}
	}
}
