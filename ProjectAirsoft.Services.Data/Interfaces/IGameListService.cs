﻿namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IGameListService
	{
		Task<bool> AddGameToUserGameListAsync(string? gameId, string userId);

		Task<bool> RemoveGameFromUserGameListAsync(string? gameId, string userId);
	}
}
