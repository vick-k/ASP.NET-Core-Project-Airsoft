using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class TerrainController(ITerrainService terrainService, ICityService cityService) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			IEnumerable<TerrainIndexViewModel> allTerrains = await terrainService.GetAllTerrainsForIndexAsync();

			return View(allTerrains);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			IEnumerable<CityListModel> cities = await cityService.GetAllCitiesForListAsync();

			TerrainFormModel viewModel = new TerrainFormModel()
			{
				Cities = cities
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TerrainFormModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			bool result = await terrainService.AddTerrainAsync(viewModel);

			if (result == false)
			{
				// add generic error message
				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
