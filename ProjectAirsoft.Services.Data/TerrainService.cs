using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Terrain;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
    public class TerrainService(ApplicationDbContext dbContext) : BaseService, ITerrainService
    {
        public async Task<IEnumerable<TerrainViewModel>> GetAllTerrainsForListAsync()
        {
            List<TerrainViewModel> terrains = await dbContext.Terrains
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Select(c => new TerrainViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                })
                .ToListAsync();

            return terrains;
        }
    }
}
