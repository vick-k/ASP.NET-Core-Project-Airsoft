﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.Web.Data;
using System.Globalization;

using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Services.Data
{
	public class GameService(ApplicationDbContext dbContext) : BaseService, IGameService
	{
		public async Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync()
		{
			List<Game> games = await dbContext.Games
				.AsNoTracking()
				.Include(g => g.Terrain)
				.ToListAsync();

			IEnumerable<GameIndexViewModel> gameIndexViewModels = games
				.Where(g => g.IsDeleted == false)
				.OrderBy(g => g.Date)
				.Select(g => new GameIndexViewModel()
				{
					Id = g.Id.ToString(),
					Name = g.Name,
					ImageUrl = g.ImageUrl != null ? g.ImageUrl : DefaultGameImage,
					Date = g.Date.ToString(CustomDateFormat),
					Terrain = g.Terrain.Name,
					IsCanceled = g.IsCanceled
				});

			return gameIndexViewModels;
		}

		public async Task<bool> AddGameAsync(GameFormViewModel viewModel, string userId)
		{
			bool isDateValid = DateTime.TryParse(viewModel.Date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

			if (!isDateValid)
			{
				return false;
			}

			Game game = new Game()
			{
				Name = viewModel.Name,
				Description = viewModel.Description,
				ImageUrl = viewModel.ImageUrl,
				Date = parsedDate,
				StartTime = viewModel.StartTime,
				Capacity = viewModel.Capacity,
				Fee = viewModel.Fee,
				TerrainId = Guid.Parse(viewModel.TerrainId),
				OrganizerId = Guid.Parse(userId)
			};

			await dbContext.Games.AddAsync(game);
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<GameDetailsViewModel> GetGameDetailsAsync(Guid id, string? userId)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Include(g => g.Terrain)
				.Include(g => g.Organizer)
				.Include(g => g.UsersGames)
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id == id);
			GameDetailsViewModel? viewModel = new GameDetailsViewModel();

			int registeredPlayers = await GetGameRegisteredPlayersCountAsync(id);

			if (game != null)
			{
				viewModel.Id = game.Id.ToString();
				viewModel.Name = game.Name;
				viewModel.Description = game.Description;
				viewModel.ImageUrl = game.ImageUrl != null ? game.ImageUrl : DefaultGameImage;
				viewModel.Date = game.Date.ToString(CustomDateFormat);
				viewModel.StartTime = game.StartTime;
				viewModel.RegisteredPlayers = registeredPlayers;
				viewModel.Capacity = game.Capacity;
				viewModel.Fee = game.Fee;
				viewModel.Terrain = game.Terrain.Name;
				viewModel.Organizer = game.Organizer.UserName!;
				viewModel.IsUserRegistered = userId == null ? false : game.UsersGames.Any(ug => ug.UserId == Guid.Parse(userId));
				viewModel.IsCanceled = game.IsCanceled;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<GameFormViewModel> GetGameForEditAsync(string id)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id == Guid.Parse(id));

			GameFormViewModel viewModel = new GameFormViewModel();

			if (game != null)
			{
				viewModel.Name = game.Name;
				viewModel.Description = game.Description;
				viewModel.ImageUrl = game.ImageUrl;
				viewModel.Date = game.Date.ToString(ISODateFormat);
				viewModel.StartTime = game.StartTime;
				viewModel.Capacity = game.Capacity;
				viewModel.Fee = game.Fee;
				viewModel.TerrainId = game.TerrainId.ToString();
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> CancelGameAsync(Guid id, string userId)
		{
			Game? game = await dbContext.Games
				.Where(g => g.IsDeleted == false &&
							g.IsCanceled == false &&
							g.OrganizerId == Guid.Parse(userId) &&
							g.Date >= DateTime.Today)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (game == null)
			{
				return false;
			}

			game.IsCanceled = true;
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> GameExistsAsync(string id)
		{
			bool result = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.AnyAsync(g => g.Id.ToString() == id);

			return result;
		}

		public async Task<bool> EditGameAsync(GameFormViewModel viewModel, Guid id)
		{
			bool isDateValid = DateTime.TryParse(viewModel.Date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

			if (!isDateValid)
			{
				return false;
			}

			int registeredPlayers = await GetGameRegisteredPlayersCountAsync(id);

			if (registeredPlayers > viewModel.Capacity)
			{
				return false;
			}

			Game? game = await dbContext.Games
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (game == null)
			{
				return false;
			}

			game.Name = viewModel.Name;
			game.Description = viewModel.Description;
			game.ImageUrl = viewModel.ImageUrl;
			game.Date = parsedDate;
			game.StartTime = viewModel.StartTime;
			game.Capacity = viewModel.Capacity;
			game.Fee = viewModel.Fee;
			game.TerrainId = Guid.Parse(viewModel.TerrainId);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<int> GetGameRegisteredPlayersCountAsync(Guid id)
		{
			return await dbContext.UsersGames
				.AsNoTracking()
				.Where(ug => ug.GameId == id)
				.CountAsync();
		}
	}
}
