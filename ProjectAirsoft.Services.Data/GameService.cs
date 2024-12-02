using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.ViewModels.Comment;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.User;
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

		public async Task<bool> AddGameAsync(GameFormModel viewModel, string userId)
		{
			bool isDateValid = DateTime.TryParse(viewModel.Date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

			if (!isDateValid)
			{
				return false;
			}

			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(viewModel.TerrainId, ref terrainGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Guid organizerGuid = Guid.Empty;
			isGuidValid = IsGuidValid(userId, ref organizerGuid);

			if (!isGuidValid)
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
				TerrainId = terrainGuid,
				OrganizerId = organizerGuid
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
			IEnumerable<CommentViewModel> comments = await GetAllCommentsAsync(id);

			CommentFormModel commentForm = new CommentFormModel()
			{
				GameId = id.ToString()
			};

			if (game != null)
			{
				bool isOrganizerDeleted = game.Organizer.IsDeleted == true;

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
				viewModel.Organizer = isOrganizerDeleted ? "(Deleted User)" : game.Organizer.UserName!;
				viewModel.IsUserRegistered = userId == null ? false : game.UsersGames.Any(ug => ug.UserId.ToString() == userId);
				viewModel.IsCanceled = game.IsCanceled;
				viewModel.Comments = comments;
				viewModel.CommentForm = commentForm;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<GameFormModel> GetGameForEditAsync(string id)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id.ToString() == id);

			GameFormModel viewModel = new GameFormModel();

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
				viewModel.OrganizerId = game.OrganizerId.ToString();
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> EditGameAsync(GameFormModel viewModel, Guid id)
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

			Guid terrainGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(viewModel.TerrainId, ref terrainGuid);

			if (!isGuidValid)
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
			game.TerrainId = terrainGuid;

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<GameCancelViewModel> GetGameForCancelAsync(string id)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Include(g => g.Terrain)
				.Include(g => g.Organizer)
				.Where(g => g.IsDeleted == false && g.IsCanceled == false)
				.FirstOrDefaultAsync(g => g.Id.ToString() == id);

			GameCancelViewModel viewModel = new GameCancelViewModel();

			if (game != null)
			{
				viewModel.Id = game.Id.ToString();
				viewModel.Name = game.Name;
				viewModel.ImageUrl = game.ImageUrl != null ? game.ImageUrl : DefaultGameImage;
				viewModel.Terrain = game.Terrain.Name;
				viewModel.Date = game.Date.ToString(CustomDateFormat);
				viewModel.Organizer = game.Organizer.UserName!;
				viewModel.OrganizerId = game.OrganizerId.ToString();
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> CancelGameAsync(Guid id, string userId)
		{
			Guid userGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Game? game = await dbContext.Games
				.Where(g => g.IsDeleted == false &&
							g.IsCanceled == false &&
							g.OrganizerId == userGuid &&
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

		public async Task<int> GetGameRegisteredPlayersCountAsync(Guid id)
		{
			return await dbContext.UsersGames
				.AsNoTracking()
				.Where(ug => ug.GameId == id)
				.CountAsync();
		}

		public async Task<int> GetGameCapacityAsync(string id)
		{
			Game game = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.FirstAsync(g => g.Id.ToString() == id);

			return game.Capacity;
		}

		public async Task<GameDeleteViewModel> GetGameForDeleteAsync(string id)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Include(g => g.Terrain)
				.Include(g => g.Organizer)
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id.ToString() == id);

			GameDeleteViewModel viewModel = new GameDeleteViewModel();

			if (game != null)
			{
				viewModel.Id = game.Id.ToString();
				viewModel.Name = game.Name;
				viewModel.ImageUrl = game.ImageUrl != null ? game.ImageUrl : DefaultGameImage;
				viewModel.Terrain = game.Terrain.Name;
				viewModel.Date = game.Date.ToString(CustomDateFormat);
				viewModel.Organizer = game.Organizer.UserName!;
				viewModel.OrganizerId = game.OrganizerId.ToString();
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> DeleteGameAsync(Guid id)
		{
			Game? game = await dbContext.Games
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (game == null)
			{
				return false;
			}

			game.IsDeleted = true;
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync(Guid id)
		{
			List<Comment> comments = await dbContext.Comments
				.AsNoTracking()
				.Include(c => c.Author)
				.Where(c => c.GameId == id)
				.ToListAsync();

			IEnumerable<CommentViewModel> viewModels = comments
				.Where(c => c.IsDeleted == false)
				.OrderBy(c => c.CreatedOn)
				.Select(c => new CommentViewModel()
				{
					Id = c.Id,
					Author = c.Author.IsDeleted ? "(Deleted User)" : c.Author.UserName!,
					CreatedOn = c.CreatedOn.ToString(CommentDateFormat),
					Content = c.Content
				});

			return viewModels;
		}

		public async Task<bool> AddCommentAsync(CommentFormModel viewModel, string userId)
		{
			Guid userGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(userId, ref userGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Guid gameGuid = Guid.Empty;
			isGuidValid = IsGuidValid(viewModel.GameId, ref gameGuid);

			if (!isGuidValid)
			{
				return false;
			}

			Comment comment = new Comment()
			{
				Content = viewModel.Content,
				CreatedOn = DateTime.Now,
				AuthorId = userGuid,
				GameId = gameGuid
			};

			await dbContext.Comments.AddAsync(comment);
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> CommentExistsAsync(int id)
		{
			bool result = await dbContext.Comments
				.AsNoTracking()
				.Where(c => c.IsDeleted == false)
				.AnyAsync(c => c.Id == id);

			return result;
		}

		public async Task<CommentDeleteViewModel> GetCommentForDeleteAsync(int id)
		{
			Comment? comment = await dbContext.Comments
				.AsNoTracking()
				.Include(c => c.Game)
				.Where(c => c.IsDeleted == false)
				.FirstOrDefaultAsync(c => c.Id == id);

			CommentDeleteViewModel viewModel = new CommentDeleteViewModel();

			if (comment != null)
			{
				viewModel.Id = comment.Id;
				viewModel.Content = comment.Content;
				viewModel.CreatedOn = comment.CreatedOn.ToString(CommentDateFormat);
				viewModel.Game = comment.Game.Name;
				viewModel.GameId = comment.Game.Id.ToString();
				viewModel.AuthorId = comment.AuthorId.ToString();
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> DeleteCommentAsync(int id)
		{
			Comment? comment = await dbContext.Comments
				.Where(c => c.IsDeleted == false)
				.FirstOrDefaultAsync(c => c.Id == id);

			if (comment == null)
			{
				return false;
			}

			comment.IsDeleted = true;
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<GameRegisteredPlayersViewModel> GetGameRegisteredPlayersAsync(Guid id)
		{
			Game? game = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.FirstOrDefaultAsync(g => g.Id == id);

			GameRegisteredPlayersViewModel viewModel = new GameRegisteredPlayersViewModel();

			if (game != null)
			{
				IEnumerable<UserViewModel> registeredPlayers = await dbContext.UsersGames
					.AsNoTracking()
					.Where(ug => ug.GameId == game.Id)
					.Select(ug => new UserViewModel()
					{
						UserName = ug.User.UserName!
					})
					.ToListAsync();

				viewModel.Name = game.Name;
				viewModel.Date = game.Date.ToString(CustomDateFormat);
				viewModel.RegisteredPlayers = registeredPlayers;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<IEnumerable<GameViewModel>> GetAllGamesForAdminAreaAsync()
		{
			List<Game> games = await dbContext.Games
				.AsNoTracking()
				.Where(g => g.IsDeleted == false)
				.Include(g => g.Terrain)
				.Include(g => g.Organizer)
				.OrderBy(g => g.Date)
				.ToListAsync();
			List<GameViewModel> gameViewModels = new List<GameViewModel>();

			foreach (Game game in games)
			{
				int registeredPlayers = await GetGameRegisteredPlayersCountAsync(game.Id);

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

			return gameViewModels;
		}
	}
}
