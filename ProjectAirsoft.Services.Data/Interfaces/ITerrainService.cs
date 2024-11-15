using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITerrainService
	{
		Task<IEnumerable<TerrainListModel>> GetAllTerrainsForListAsync();

		Task<IEnumerable<TerrainIndexViewModel>> GetAllTerrainsForIndexAsync();

		Task<bool> AddTerrainAsync(TerrainFormModel viewModel);
	}
}
