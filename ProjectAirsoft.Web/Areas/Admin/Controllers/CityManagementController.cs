using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.ViewModels.City;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CityManagementController(ICityService cityService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<CityViewModel> cities = await cityService.GetAllCitiesForAdminAreaAsync();

			return View(cities);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			CityFormModel viewModel = await cityService.GetCityForEditAsync(id);

			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CityFormModel viewModel, int id)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			bool cityExists = await cityService.CityExistsAsync(id);

			if (!cityExists)
			{
				// add error message
				return RedirectToAction(nameof(Index));
			}

			bool isEdited = await cityService.EditCityAsync(viewModel, id);

			if (!isEdited)
			{
				// add error message
				return View(viewModel);
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Create()
		{
			CityInputViewModel viewModel = new CityInputViewModel();

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CityInputViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			bool result = await cityService.AddCityAsync(viewModel);

			if (result == false)
			{
				// add error message
				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
