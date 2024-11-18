using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class TerrainController(IBaseService baseService, ITerrainService terrainService, ICityService cityService) : Controller
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

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			TerrainFormModel viewModel = await terrainService.GetTerrainForEditAsync(id);

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
		public async Task<IActionResult> Edit(TerrainFormModel viewModel, string id)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			if (!ModelState.IsValid)
			{
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			bool terrainExists = await terrainService.TerrainExistsAsync(id);

			if (!terrainExists)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await terrainService.EditTerrainAsync(viewModel, terrainGuid);

			if (!isEdited)
			{
				// add generic error message
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();
				return View(viewModel);
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			TerrainDeleteViewModel viewModel = await terrainService.GetTerrainForDeleteAsync(id); // TODO: change the parameter to guid

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(TerrainDeleteViewModel viewModel)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await terrainService.DeleteTerrainAsync(terrainGuid);

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
