using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.ViewModels.Terrain;
using ProjectAirsoft.Web.Areas.Admin.ViewModels;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TerrainManagementController(ApplicationDbContext dbContext, IBaseService baseService, ITerrainService terrainService, ICityService cityService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<Terrain> terrains = await dbContext.Terrains
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.OrderBy(t => t.Name)
				.ToListAsync();
			List<TerrainViewModel> terrainViewModels = new List<TerrainViewModel>();

			foreach (Terrain terrain in terrains)
			{
				terrainViewModels.Add(new TerrainViewModel()
				{
					Id = terrain.Id.ToString(),
					Name = terrain.Name,
					Location = terrain.LocationUrl,
					City = terrain.City.Name,
					IsActive = terrain.IsActive,
					IsDeleted = terrain.IsDeleted
				});
			}

			return View(terrainViewModels);
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
				// add error message
				return RedirectToAction(nameof(Index));
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
				// add error message
				return RedirectToAction(nameof(Index));
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}
	}
}
