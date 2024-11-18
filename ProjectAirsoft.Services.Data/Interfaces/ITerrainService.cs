using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITerrainService
	{
		Task<IEnumerable<TerrainListModel>> GetAllTerrainsForListAsync();

		Task<IEnumerable<TerrainIndexViewModel>> GetAllTerrainsForIndexAsync();

		Task<bool> AddTerrainAsync(TerrainFormModel viewModel);

		Task<TerrainFormModel> GetTerrainForEditAsync(string id);

		Task<bool> EditTerrainAsync(TerrainFormModel viewModel, Guid id);

		Task<bool> TerrainExistsAsync(string id);
	}
}
