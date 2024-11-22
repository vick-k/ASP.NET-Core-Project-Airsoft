using ProjectAirsoft.ViewModels.Team;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface ITeamService
	{
		Task<bool> AddTeamAsync(TeamFormModel viewModel, string userId);
	}
}
