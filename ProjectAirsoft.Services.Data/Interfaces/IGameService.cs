using ProjectAirsoft.ViewModels.Comment;
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

		Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync(Guid id);

		Task<bool> AddCommentAsync(CommentFormModel viewModel, string userId);

		Task<bool> CommentExistsAsync(int id);

		Task<CommentDeleteViewModel> GetCommentForDeleteAsync(int id);

		Task<bool> DeleteCommentAsync(int id);
	}
}
