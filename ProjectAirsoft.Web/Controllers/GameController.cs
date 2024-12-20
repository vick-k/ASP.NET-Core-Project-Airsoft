﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.Terrain;

using static ProjectAirsoft.Common.AlertMessages.Game;
using static ProjectAirsoft.Common.ApplicationConstants;
using static ProjectAirsoft.Common.ValidationMessages.Game;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class GameController(IBaseService baseService, IGameService gameService, ITerrainService terrainService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index(AllGamesFilterViewModel viewModel)
		{
			IEnumerable<GameIndexViewModel> allGames = await gameService.GetAllGamesAsync(viewModel);
			int allGamesCount = await gameService.GetGamesCountByFilterAsync(viewModel);

			IEnumerable<TerrainListModel> terrains = await terrainService.GetAllTerrainsForListAsync();
			List<string> terrainNames = new List<string>();

			foreach (TerrainListModel t in terrains)
			{
				terrainNames.Add(t.Name);
			}

			AllGamesFilterViewModel filteredViewModel = new AllGamesFilterViewModel()
			{
				Games = allGames,
				TerrainFilter = viewModel.TerrainFilter,
				AllTerrains = terrainNames,
				CurrentPage = viewModel.CurrentPage,
				EntitiesPerPage = viewModel.EntitiesPerPage,
				TotalPages = (int)Math.Ceiling((double)allGamesCount / viewModel.EntitiesPerPage!.Value)
			};

			return View(filteredViewModel);
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Create()
		{
			IEnumerable<TerrainListModel> terrains = await terrainService.GetAllTerrainsForListAsync();

			GameFormModel viewModel = new GameFormModel()
			{
				Terrains = terrains
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Create(GameFormModel viewModel)
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
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();
				TempData[AlertDanger] = AddGameFailMessage;

				return View(viewModel);
			}

			TempData[AlertSuccess] = AddGameSuccessMessage;

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
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Edit(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameFormModel viewModel = await gameService.GetGameForEditAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			string? userId = userManager.GetUserId(User);

			if (viewModel.OrganizerId != userId)
			{
				TempData[AlertDanger] = EditGameNotOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			IEnumerable<TerrainListModel> terrains = await terrainService.GetAllTerrainsForListAsync();
			viewModel.Terrains = terrains;

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Edit(GameFormModel viewModel)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref gameGuid);

			if (!isGuidValid || viewModel.Id == null)
			{
				TempData[AlertDanger] = EditGameGenericMessage;

				return RedirectToAction(nameof(Index));
			}

			if (!ModelState.IsValid)
			{
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();

				return View(viewModel);
			}

			bool gameExists = await gameService.GameExistsAsync(viewModel.Id);

			if (gameExists == false)
			{
				TempData[AlertDanger] = GameDoesNotExistMessage;

				return RedirectToAction(nameof(Index));
			}

			string? userId = userManager.GetUserId(User);

			GameFormModel gameFromEditForm = await gameService.GetGameForEditAsync(viewModel.Id);

			if (gameFromEditForm.OrganizerId != userId)
			{
				TempData[AlertDanger] = EditGameNotOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			if (viewModel.OrganizerId != userId)
			{
				TempData[AlertDanger] = EditGameNotOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await gameService.EditGameAsync(viewModel, gameGuid);

			if (isEdited == false)
			{
				int registeredPlayers = await gameService.GetGameRegisteredPlayersCountAsync(gameGuid);

				if (registeredPlayers > viewModel.Capacity)
				{
					ModelState.AddModelError(nameof(viewModel.Capacity), CapacityLessThanRegisteredPlayers);
					viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();

					return View(viewModel);
				}

				TempData[AlertDanger] = EditGameGenericMessage;
				viewModel.Terrains = await terrainService.GetAllTerrainsForListAsync();

				return View(viewModel);
			}

			TempData[AlertSuccess] = EditGameSuccessMessage;

			return RedirectToAction(nameof(Details), new { id = viewModel.Id });
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Cancel(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameCancelViewModel viewModel = await gameService.GetGameForCancelAsync(id);
			string userId = userManager.GetUserId(User)!;

			if (viewModel == null || userId != viewModel.OrganizerId)
			{
				TempData[AlertDanger] = GameGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Cancel(GameDeleteViewModel viewModel)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string userId = userManager.GetUserId(User)!;

			bool result = await gameService.CancelGameAsync(gameGuid, userId);

			if (result == false)
			{
				TempData[AlertDanger] = GameGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = GameCancelSuccessMessage;

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Delete(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameDeleteViewModel viewModel = await gameService.GetGameForDeleteAsync(id);
			string userId = userManager.GetUserId(User)!;

			if (viewModel == null || userId != viewModel.OrganizerId)
			{
				TempData[AlertDanger] = GameGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Manager")]
		public async Task<IActionResult> Delete(GameDeleteViewModel viewModel)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(viewModel.Id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			string userId = userManager.GetUserId(User)!;
			GameDeleteViewModel gameFromDeleteForm = await gameService.GetGameForDeleteAsync(viewModel.Id);

			if (gameFromDeleteForm.OrganizerId != userId)
			{
				TempData[AlertDanger] = DeleteGameNowOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			if (userId != viewModel.OrganizerId)
			{
				TempData[AlertDanger] = DeleteGameNowOwnerMessage;

				return RedirectToAction(nameof(Index));
			}

			bool result = await gameService.DeleteGameAsync(gameGuid);

			if (result == false)
			{
				TempData[AlertDanger] = GameGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = DeleteGameSuccessMessage;

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> RegisteredPlayers(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			GameRegisteredPlayersViewModel viewModel = await gameService.GetGameRegisteredPlayersAsync(gameGuid);

			if (viewModel == null)
			{
				TempData[AlertDanger] = GameGenericErrorMessage;

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}
	}
}
