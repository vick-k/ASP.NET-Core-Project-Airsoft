using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.Terrain;

using static ProjectAirsoft.Common.ValidationMessages.Game;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class GameController(IBaseService baseService, IGameService gameService, ITerrainService terrainService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			IEnumerable<GameIndexViewModel> allGames = await gameService.GetAllGamesAsync();

			return View(allGames);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			IEnumerable<TerrainViewModel> terrains = await terrainService.GetAllTerrainsForListAsync();

			GameFormViewModel viewModel = new GameFormViewModel()
			{
				Terrains = terrains
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(GameFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();
				return View(viewModel);
			}

			string userId = userManager.GetUserId(User)!;
			bool result = await gameService.AddGameAsync(viewModel, userId);

			if (result == false)
			{
				// add generic error message
				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string? userId = userManager.GetUserId(User);

			GameDetailsViewModel viewModel = await gameService.GetGameDetailsAsync(gameGuid, userId);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameFormViewModel viewModel = await gameService.GetGameForEditAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			IEnumerable<TerrainViewModel> terrains = await terrainService.GetAllTerrainsForListAsync();
			viewModel.Terrains = terrains;

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(GameFormViewModel viewModel, string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			if (!ModelState.IsValid)
			{
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();
				return View(viewModel);
			}

			bool gameExists = await gameService.GameExistsAsync(id);

			if (gameExists == false)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await gameService.EditGameAsync(viewModel, gameGuid);

			if (isEdited == false)
			{
				int registeredPlayers = await gameService.GetGameRegisteredPlayersCountAsync(gameGuid);

				if (registeredPlayers > viewModel.Capacity)
				{
					ModelState.AddModelError(nameof(viewModel.Capacity), CapacityLessThanRegisteredPlayers);
					return View(viewModel);
				}

				// add generic error message
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();
				return View(viewModel);
			}

			// add success message
			return RedirectToAction(nameof(Details), new { id });
		}

		[HttpPost]
		public async Task<IActionResult> Cancel(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string userId = userManager.GetUserId(User)!;

			bool result = await gameService.CancelGameAsync(gameGuid, userId);

			if (result == false)
			{
				// add generic error message
				return RedirectToAction(nameof(Index));
			}

			// add cancel success message
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameDeleteViewModel viewModel = await gameService.GetGameForDeleteAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(GameDeleteViewModel viewModel)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await gameService.DeleteGameAsync(gameGuid);

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
