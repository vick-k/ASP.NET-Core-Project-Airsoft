using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;

namespace ProjectAirsoft.Web.Controllers
{
	[AllowAnonymous]
	public class HomeController(IGameService gameService) : Controller
	{
		public async Task<IActionResult> Index()
		{
			IEnumerable<GameIndexViewModel> upcomingGames = await gameService.GetUpcomingGamesAsync();

			return View(upcomingGames);
		}

		public IActionResult Error(int? statusCode = null)
		{
			if (statusCode.HasValue)
			{
				if (statusCode == 404)
				{
					return View("Error404");
				}

				if (statusCode == 500)
				{
					return View("Error500");
				}
			}

			return View("Error");
		}
	}
}
