using ProjectAirsoft.ViewModels.City;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ICityService
	{
		Task<IEnumerable<CityListModel>> GetAllCitiesForListAsync();
	}
}
