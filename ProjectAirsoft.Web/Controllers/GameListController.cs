using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class GameListController(IGameListService gameListService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddToGameList(string? id)
		{
			string userId = userManager.GetUserId(User)!;
			bool result = await gameListService.AddGameToUserGameListAsync(id, userId);

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
