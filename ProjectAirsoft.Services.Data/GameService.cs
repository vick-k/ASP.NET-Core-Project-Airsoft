using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.Terrain;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Services.Data
{
	public class GameService(ApplicationDbContext dbContext) : IGameService
	{
		public async Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync()
		{
			List<Game> games = await dbContext.Games
				//.Include(g => g.Terrain) // include if needed
				.ToListAsync();

			IEnumerable<GameIndexViewModel> gameIndexViewModels = games
				.Where(g => g.IsDeleted == false)
				.Select(g => new GameIndexViewModel()
				{
					Id = g.Id.ToString(),
					Name = g.Name,
					ImageUrl = g.ImageUrl,
					Date = g.Date.ToString(CustomDateFormat),
					Terrain = g.Terrain.Name
				});

			return gameIndexViewModels;
		}
	}
}
