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
		public async Task<IActionResult> Details(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string? userId = userManager.GetUserId(User);

			TeamDetailsViewModel viewModel = await teamService.GetTeamDetailsAsync(teamGuid);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}
	}
}
