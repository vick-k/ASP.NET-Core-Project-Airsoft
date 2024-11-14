using ProjectAirsoft.ViewModels.Game;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IGameService
	{
		Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync();

		Task<bool> AddGameAsync(GameFormViewModel viewModel, string userId);

		Task<GameDetailsViewModel> GetGameDetailsAsync(Guid id, string? userId);

		Task<GameFormViewModel> GetGameForEditAsync(string id);

		Task<bool> EditGameAsync(GameFormViewModel viewModel, Guid id);

		Task<bool> GameExistsAsync(string id);

		Task<bool> CancelGameAsync(Guid id, string userId);

		Task<int> GetGameRegisteredPlayersCountAsync(Guid id);

		Task<int> GetGameCapacityAsync(string id);
	}
}
