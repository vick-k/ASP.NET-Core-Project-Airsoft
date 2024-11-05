using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;

namespace ProjectAirsoft.Web.Controllers
{
	public class GameController(IGameService gameService, ITerrainService terrainService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var allGames = await gameService.GetAllGamesAsync();

			return View(allGames);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var terrains = await terrainService.GetAllTerrainsForListAsync();

			var viewModel = new GameCreateViewModel()
			{
				Terrains = terrains
			};

			return View(viewModel);
		}
	}
}
