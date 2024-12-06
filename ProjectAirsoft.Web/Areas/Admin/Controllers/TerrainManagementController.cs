using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Terrain;

using static ProjectAirsoft.Common.AlertMessages.Terrain;
using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TerrainManagementController(IBaseService baseService, ITerrainService terrainService, ICityService cityService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<TerrainViewModel> terrains = await terrainService.GetAllTerrainsForAdminAreaAsync();

			return View(terrains);
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
				TempData[AlertDanger] = TerrainDuplicateMessage;
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();

				return View(viewModel);
			}

			TempData[AlertSuccess] = AddTerrainSuccessMessage;

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

			viewModel.Cities = await cityService.GetAllCitiesForListAsync();

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
				TempData[AlertDanger] = TerrainDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await terrainService.EditTerrainAsync(viewModel, terrainGuid);

			if (!isEdited)
			{
				TempData[AlertDanger] = EditTerrainFailMessage;
				viewModel.Cities = await cityService.GetAllCitiesForListAsync();

				return View(viewModel);
			}

			TempData[AlertSuccess] = EditTerrainSuccessMessage;

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Status(string id)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			TerrainStatusViewModel viewModel = await terrainService.GetTerrainForStatusChangeAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Status(TerrainStatusViewModel viewModel)
		{
			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref terrainGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await terrainService.TerrainStatusChangeAsync(terrainGuid);

			if (result == false)
			{
				TempData[AlertDanger] = TerrainChangeStatusFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = TerrainChangeStatusSuccessMessage;

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

			TerrainDeleteViewModel viewModel = await terrainService.GetTerrainForDeleteAsync(id);

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
				TempData[AlertDanger] = DeleteTerrainFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = DeleteTerrainSuccessMessage;

			return RedirectToAction(nameof(Index));
		}
	}
}
