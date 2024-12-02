using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class GameManagementController(IBaseService baseService, IGameService gameService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<GameViewModel> games = await gameService.GetAllGamesForAdminAreaAsync();

			return View(games);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteGame(string id)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref gameGuid);

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
