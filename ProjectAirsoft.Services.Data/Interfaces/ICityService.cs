using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.ViewModels.City;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ICityService
	{
		Task<IEnumerable<CityListModel>> GetAllCitiesForListAsync();

		Task<IEnumerable<CityViewModel>> GetAllCitiesForAdminAreaAsync();

		Task<CityFormModel> GetCityForEditAsync(int id);

		Task<bool> EditCityAsync(CityFormModel viewModel, int id);

		Task<bool> CityExistsAsync(int id);
	}
}
