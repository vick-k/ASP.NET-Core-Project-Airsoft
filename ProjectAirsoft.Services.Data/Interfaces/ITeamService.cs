using ProjectAirsoft.ViewModels.Team;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITeamService
	{
		Task<IEnumerable<TeamIndexViewModel>> GetAllTeamsAsync();

		Task<bool> AddTeamAsync(TeamFormModel viewModel, string userId);

		Task<TeamDetailsViewModel> GetTeamDetailsAsync(Guid id);
	}
}
