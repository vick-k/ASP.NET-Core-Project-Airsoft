using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
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
				.OrderBy(c => c.Name)
				.Select(c => new CityListModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

			return cities;
		}

		public async Task<IEnumerable<CityViewModel>> GetAllCitiesForAdminAreaAsync()
		{
			List<CityViewModel> cities = await dbContext.Cities
				.AsNoTracking()
				.OrderBy(c => c.Name)
				.Select(c => new CityViewModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

			return cities;
		}

		public async Task<CityFormModel> GetCityForEditAsync(int id)
		{
			City? city = await dbContext.Cities
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);

			CityFormModel viewModel = new CityFormModel();

			if (city != null)
			{
				viewModel.Name = city.Name;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> EditCityAsync(CityFormModel viewModel, int id)
		{
			City? city = await dbContext.Cities
				.FirstOrDefaultAsync(c => c.Id == id);

			if (city == null)
			{
				return false;
			}

			city.Name = viewModel.Name;

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> CityExistsAsync(int id)
		{
			bool result = await dbContext.Cities
				.AsNoTracking()
				.AnyAsync(c => c.Id == id);

			return result;
		}

		public async Task<bool> AddCityAsync(CityInputViewModel viewModel)
		{
			string[]? cityNames = viewModel.CityNames
				.Split(Environment.NewLine);

			foreach (string city in cityNames.Distinct())
			{
				bool cityExists = await dbContext.Cities
					.AsNoTracking()
					.AnyAsync(c => c.Name == city);

				if (cityExists)
				{
					return false;
				}

				string trimmedCity = city.Trim();

				City newCity = new City()
				{
					Name = trimmedCity
				};

				await dbContext.Cities.AddAsync(newCity);
			}

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
