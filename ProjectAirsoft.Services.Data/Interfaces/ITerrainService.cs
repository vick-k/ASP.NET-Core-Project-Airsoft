using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITerrainService
	{
		Task<IEnumerable<TerrainListModel>> GetAllTerrainsForListAsync();

		Task<bool> AddTerrainAsync(TerrainFormModel viewModel);
	}
}
