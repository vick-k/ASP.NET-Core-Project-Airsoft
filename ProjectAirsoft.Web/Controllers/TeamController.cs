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
	public class TeamController(ITeamService teamService, ICityService cityService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
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
	}
}
