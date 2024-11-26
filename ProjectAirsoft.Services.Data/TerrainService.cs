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
				.Select(t => new TerrainListModel()
				{
					Id = t.Id.ToString(),
					Name = t.Name
				})
				.ToListAsync();

			return terrains;
		}

		public async Task<IEnumerable<TerrainIndexViewModel>> GetAllTerrainsForIndexAsync()
		{
			List<Terrain> terrains = await dbContext.Terrains
				.AsNoTracking()
				.Include(t => t.City)
				.Include(t => t.Games)
				.ToListAsync();

			IEnumerable<TerrainIndexViewModel> terrainIndexViewModels = terrains
				.OrderBy(t => t.Name)
				.Select(t => new TerrainIndexViewModel()
				{
					Id = t.Id.ToString(),
					Name = t.Name,
					LocationUrl = t.LocationUrl,
					City = t.City.Name,
					GamesCount = t.Games
						.Where(g => g.IsDeleted == false)
						.Count(),
					IsDeleted = t.IsDeleted
				});

			return terrainIndexViewModels;
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

		public async Task<TerrainFormModel> GetTerrainForEditAsync(string id)
		{
			Terrain? terrain = await dbContext.Terrains
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id.ToString() == id);

			TerrainFormModel viewModel = new TerrainFormModel();

			if (terrain != null)
			{
				viewModel.Name = terrain.Name;
				viewModel.LocationUrl = terrain.LocationUrl;
				viewModel.CityId = terrain.CityId;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> TerrainExistsAsync(string id)
		{
			bool result = await dbContext.Terrains
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.AnyAsync(t => t.Id.ToString() == id);

			return result;
		}

		public async Task<bool> EditTerrainAsync(TerrainFormModel viewModel, Guid id)
		{
			Terrain? terrain = await dbContext.Terrains
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (terrain == null)
			{
				return false;
			}

			terrain.Name = viewModel.Name;
			terrain.LocationUrl = viewModel.LocationUrl;
			terrain.CityId = viewModel.CityId;

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<TerrainDeleteViewModel> GetTerrainForDeleteAsync(string id)
		{
			Terrain? terrain = await dbContext.Terrains
				.AsNoTracking()
				.Include(t => t.City)
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id.ToString() == id);

			TerrainDeleteViewModel viewModel = new TerrainDeleteViewModel();

			if (terrain != null)
			{
				viewModel.Id = terrain.Id.ToString();
				viewModel.Name = terrain.Name;
				viewModel.City = terrain.City.Name;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> DeleteTerrainAsync(Guid id)
		{
			Terrain? terrain = await dbContext.Terrains
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (terrain == null)
			{
				return false;
			}

			terrain.IsDeleted = true;
			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
