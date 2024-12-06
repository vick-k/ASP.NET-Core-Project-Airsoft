using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;

using static ProjectAirsoft.Common.AlertMessages.Team;
using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TeamManagementController(IBaseService baseService, ITeamService teamService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<TeamViewModel> teams = await teamService.GetAllTeamsForAdminAreaAsync();

			return View(teams);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteTeam(string id)
		{
			Guid teamGuid = Guid.Empty;
			bool isGuidValid = baseService.IsGuidValid(id, ref teamGuid);

			if (!isGuidValid)
			{
				return RedirectToAction(nameof(Index));
			}

			bool result = await teamService.DeleteTeamAsync(teamGuid);

			if (result == false)
			{
				TempData[AlertDanger] = DeleteTeamFailMessage;

				return RedirectToAction(nameof(Index));
			}

			TempData[AlertSuccess] = DeleteTeamSuccessMessage;

			return RedirectToAction(nameof(Index));
		}
	}
}
