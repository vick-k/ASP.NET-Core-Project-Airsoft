using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.Web.Data;
using System.Globalization;
using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Services.Data
{
    public class GameService(ApplicationDbContext dbContext) : IGameService
    {
        public async Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync()
        {
            List<Game> games = await dbContext.Games
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
                    Terrain = g.Terrain.Name
                });

            return gameIndexViewModels;
        }

        public async Task<bool> AddGameAsync(GameCreateViewModel viewModel, string userId)
        {
            bool isDateValid = DateTime.TryParse(viewModel.Date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

            if (!isDateValid)
            {
                return false;
            }

            var game = new Game()
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
    }
}
