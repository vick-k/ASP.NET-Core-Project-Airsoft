using ProjectAirsoft.ViewModels.Game;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IGameService
	{
		Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync();

		Task<bool> AddGameAsync(GameFormModel viewModel, string userId);

		Task<GameDetailsViewModel> GetGameDetailsAsync(Guid id, string? userId);

		Task<GameFormModel> GetGameForEditAsync(string id);

		Task<bool> EditGameAsync(GameFormModel viewModel, Guid id);

		Task<bool> GameExistsAsync(string id);

		Task<bool> CancelGameAsync(Guid id, string userId);

		Task<int> GetGameRegisteredPlayersCountAsync(Guid id);

		Task<int> GetGameCapacityAsync(string id);

		Task<GameDeleteViewModel> GetGameForDeleteAsync(string id);

		Task<bool> DeleteGameAsync(Guid id);
	}
}
