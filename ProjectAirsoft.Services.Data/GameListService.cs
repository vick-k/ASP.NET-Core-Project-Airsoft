﻿using Microsoft.EntityFrameworkCore;
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
				.FindAsync(gameGuid);

			if (game == null)
			{
				return false;
			}

			Guid userGuid = Guid.Parse(userId); // should I check for a valid Guid?

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
	}
}