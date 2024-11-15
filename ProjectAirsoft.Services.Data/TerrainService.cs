using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Terrain;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class TerrainService(ApplicationDbContext dbContext) : BaseService, ITerrainService
	{
		public async Task<IEnumerable<TerrainListModel>> GetAllTerrainsForListAsync()
		{
			List<TerrainListModel> terrains = await dbContext.Terrains
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.Select(c => new TerrainListModel()
				{
					Id = c.Id.ToString(),
					Name = c.Name
				})
				.ToListAsync();

			return terrains;
		}

		public async Task<bool> AddTerrainAsync(TerrainFormModel viewModel)
		{
			Terrain terrain = new Terrain()
			{
				Name = viewModel.Name,
				LocationUrl = viewModel.LocationUrl,
				CityId = viewModel.CityId
			};

			await dbContext.Terrains.AddAsync(terrain);
			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
