using ProjectAirsoft.ViewModels.AdminArea;
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

		Task<TerrainStatusViewModel> GetTerrainForStatusChangeAsync(string id);

		Task<bool> TerrainStatusChangeAsync(Guid id);

		Task<TerrainDeleteViewModel> GetTerrainForDeleteAsync(string id);

		Task<bool> DeleteTerrainAsync(Guid id);

		Task<IEnumerable<TerrainViewModel>> GetAllTerrainsForAdminAreaAsync();
	}
}
