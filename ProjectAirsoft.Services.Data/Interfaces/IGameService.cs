using ProjectAirsoft.ViewModels.Game;
using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IGameService
	{
		Task<IEnumerable<GameIndexViewModel>> GetAllGamesAsync();
	}
}
