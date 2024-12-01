using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.Web.Areas.Admin.ViewModels;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class GameManagementController(ApplicationDbContext dbContext, IBaseService baseService, IGameService gameService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<Game> games = await dbContext.Games
				.Where(g => g.IsDeleted == false)
				.Include(g => g.Terrain)
				.Include(g => g.Organizer)
				.OrderBy(g => g.Date)
				.ToListAsync();
			List<GameViewModel> gameViewModels = new List<GameViewModel>();

			foreach (Game game in games)
			{
				int registeredPlayers = await gameService.GetGameRegisteredPlayersCountAsync(game.Id);

				gameViewModels.Add(new GameViewModel()
				{
					Id = game.Id.ToString(),
					Name = game.Name,
					Date = game.Date.ToString(CustomDateFormat),
					Capacity = game.Capacity,
					RegisteredPlayers = registeredPlayers,
					Fee = game.Fee,
					Terrain = game.Terrain.Name,
					Organizer = game.Organizer.UserName!,
					IsDeleted = game.IsDeleted
				});
			}

			return View(gameViewModels);
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
