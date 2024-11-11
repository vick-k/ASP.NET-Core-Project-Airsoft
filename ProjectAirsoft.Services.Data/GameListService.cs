using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.Web.Data;

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
	}
}
