using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.Web.Areas.Admin.ViewModels;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TeamManagementController(ApplicationDbContext dbContext, IBaseService baseService, ITeamService teamService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<Team> teams = await dbContext.Teams
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.Include(t => t.Leader)
				.OrderBy(t => t.Name)
				.ToListAsync();
			List<TeamViewModel> teamViewModels = new List<TeamViewModel>();

			foreach (Team team in teams)
			{
				teamViewModels.Add(new TeamViewModel()
				{
					Id = team.Id.ToString(),
					Name = team.Name,
					City = team.City.Name,
					Leader = team.Leader.UserName!,
					IsDeleted = team.IsDeleted
				});
			}

			return View(teamViewModels);
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
				// add error message
				return RedirectToAction(nameof(Index));
			}

			// add success message
			return RedirectToAction(nameof(Index));
		}
	}
}
