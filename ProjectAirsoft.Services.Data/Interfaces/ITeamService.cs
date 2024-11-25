using ProjectAirsoft.Data.Models;
using ProjectAirsoft.ViewModels.Team;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITeamService
	{
		Task<IEnumerable<TeamIndexViewModel>> GetAllTeamsAsync();

		Task<bool> AddTeamAsync(TeamFormModel viewModel, string userId);

		Task<TeamDetailsViewModel> GetTeamDetailsAsync(Guid id);

		Task<TeamJoinViewModel> GetTeamJoinAsync(Guid id);

		Task<bool> JoinTeamAsync(Guid id, ApplicationUser user);

		Task<TeamLeaveViewModel> GetTeamLeaveAsync(Guid id);

		Task<bool> LeaveTeamAsync(Guid id, ApplicationUser user);

		Task<TeamFormModel> GetTeamForEditAsync(string teamId, string userId);

		Task<bool> EditTeamAsync(TeamFormModel viewModel, Guid id);

		Task<bool> TeamExistsAsync(string id);
	}
}
