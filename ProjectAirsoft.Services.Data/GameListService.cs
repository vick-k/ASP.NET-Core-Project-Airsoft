using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.GameList;
using ProjectAirsoft.Web.Data;

using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Services.Data
{
	public class GameListService(ApplicationDbContext dbContext) : BaseService, IGameListService
	{
		public async Task<bool> AddGameToUserGameListAsync(string? gameId, string userId)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(gameId, ref gameGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Game? game = await dbContext.Games
				.Where(g => g.IsCanceled == false && g.Date >= DateTime.Today)
				.FirstOrDefaultAsync(g => g.Id == gameGuid);

			if (game == null)
			{
				return false;
			}

			int registeredPlayers = await dbContext.UsersGames
				.AsNoTracking()
				.Where(ug => ug.GameId == gameGuid)
				.CountAsync();

			if (registeredPlayers == game.Capacity)
			{
				return false;
			}

			Guid userGuid = Guid.Empty;
			isGuidValid = IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return false;
			}

			UserGame? userGame = await dbContext.UsersGames
				.FirstOrDefaultAsync(ug => ug.UserId == userGuid && ug.GameId == gameGuid);

			if (userGame == null)
			{
				UserGame newUserGame = new UserGame()
				{
					UserId = userGuid,
					GameId = gameGuid
				};

				await dbContext.UsersGames.AddAsync(newUserGame);
				await dbContext.SaveChangesAsync();
			}
			else
			{
				return false;
			}

			return true;
		}

		public async Task<bool> RemoveGameFromUserGameListAsync(string? gameId, string userId)
		{
			Guid gameGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(gameId, ref gameGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Game? game = await dbContext.Games
				.Where(g => g.IsCanceled == false && g.Date >= DateTime.Today)
				.FirstOrDefaultAsync(g => g.Id == gameGuid);

			if (game == null)
			{
				return false;
			}

			Guid userGuid = Guid.Empty;
			isGuidValid = IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return false;
			}

			UserGame? userGame = await dbContext.UsersGames
				.FirstOrDefaultAsync(ug => ug.UserId == userGuid && ug.GameId == gameGuid);

			if (userGame != null)
			{
				dbContext.UsersGames.Remove(userGame);
				await dbContext.SaveChangesAsync();
			}
			else
			{
				return false;
			}

			return true;
		}

		public async Task<IEnumerable<GameListViewModel>> GetGameListAsync(string userId)
		{
			List<GameListViewModel> gameList = await dbContext.UsersGames
				.Include(ug => ug.Game)
				.Where(ug => ug.UserId.ToString() == userId)
				.Where(ug => ug.Game.IsDeleted == false && ug.Game.IsCanceled == false)
				.OrderBy(ug => ug.Game.Date)
				.Select(ug => new GameListViewModel()
				{
					Id = ug.GameId.ToString(),
					ImageUrl = ug.Game.ImageUrl != null ? ug.Game.ImageUrl : DefaultGameImage,
					Name = ug.Game.Name,
					Date = ug.Game.Date.ToString(CustomDateFormat),
					StartTime = ug.Game.StartTime,
					Terrain = ug.Game.Terrain.Name
				})
				.ToListAsync();

			return gameList;
		}
	}
}
