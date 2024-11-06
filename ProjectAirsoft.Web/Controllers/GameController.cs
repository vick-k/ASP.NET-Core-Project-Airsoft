using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;

using static ProjectAirsoft.Common.ValidationMessages.Game;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class GameController(IGameService gameService, ITerrainService terrainService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var allGames = await gameService.GetAllGamesAsync();

			return View(allGames);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var terrains = await terrainService.GetAllTerrainsForListAsync();

			var viewModel = new GameCreateViewModel()
			{
				Terrains = terrains
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(GameCreateViewModel viewModel)
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
				ModelState.AddModelError(nameof(viewModel.Date), InvalidDateMessage);

				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
