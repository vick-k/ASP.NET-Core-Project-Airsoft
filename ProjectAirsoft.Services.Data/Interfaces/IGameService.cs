using ProjectAirsoft.ViewModels.Game;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IGameService
	{
		Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync();

		Task<bool> AddGameAsync(GameCreateViewModel viewModel, string userId);

		Task<GameDetailsViewModel> GetGameDetailsAsync(Guid id, string? userId);

		Task<bool> CancelGameAsync(Guid id, string userId);
	}
}
