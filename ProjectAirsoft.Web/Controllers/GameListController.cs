using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.GameList;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class GameListController(IGameListService gameListService, IGameService gameService, IBaseService baseService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string userId = userManager.GetUserId(User)!;

			IEnumerable<GameListViewModel> gameList = await gameListService.GetGameListAsync(userId);

			return View(gameList);
		}

		[HttpPost]
		public async Task<IActionResult> AddToGameList(string? id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

			if (!isGuidValid)
			{
				// add error message
				return RedirectToAction("Index", "Game");
			}

			string userId = userManager.GetUserId(User)!;
			bool result = await gameListService.AddGameToUserGameListAsync(id, userId);

			if (result == false)
			{
				int registeredPlayers = await gameService.GetGameRegisteredPlayersCountAsync(gameGuid);
				int gameCapacity = await gameService.GetGameCapacityAsync(id!);

				if (registeredPlayers == gameCapacity)
				{
					// add error message
					return RedirectToAction("Details", "Game", new { id });
				}

				// add generic error message
				return RedirectToAction("Details", "Game", new { id });
			}

			// add successful message
			return RedirectToAction("Index", "Game");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromGameList(string? id)
		{
			string userId = userManager.GetUserId(User)!;
			bool result = await gameListService.RemoveGameFromUserGameListAsync(id, userId);

			if (result == false)
			{
				// add error message
				return RedirectToAction("Details", "Game", new { id });
			}

			// add successful message
			return RedirectToAction("Index", "Game");
		}
	}
}
