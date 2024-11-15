using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.City;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class CityService(ApplicationDbContext dbContext) : ICityService
	{
		public async Task<IEnumerable<CityListModel>> GetAllCitiesForListAsync()
		{
			List<CityListModel> cities = await dbContext.Cities
				.AsNoTracking()
				.Select(c => new CityListModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

			return cities;
		}
	}
}
