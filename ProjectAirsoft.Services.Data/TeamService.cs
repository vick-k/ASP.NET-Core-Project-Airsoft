using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Team;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class TeamService(ApplicationDbContext dbContext) : BaseService, ITeamService
	{
		public async Task<bool> AddTeamAsync(TeamFormModel viewModel, string userId)
		{
			Guid leaderGuid = Guid.Empty;
			bool isGuidValid = IsGuidValid(userId, ref leaderGuid);

			if (!isGuidValid)
			{
				return false;
			}

			ApplicationUser? currentUser = await dbContext.Users
				.FirstOrDefaultAsync(u => u.Id == leaderGuid);

			if (currentUser == null || currentUser.TeamId != null)
			{
				return false;
			}

			Team team = new Team()
			{
				Name = viewModel.Name,
				LogoUrl = viewModel.LogoUrl,
				CityId = viewModel.CityId,
				LeaderId = leaderGuid
			};

			currentUser.TeamId = team.Id;

			await dbContext.Teams.AddAsync(team);
			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
