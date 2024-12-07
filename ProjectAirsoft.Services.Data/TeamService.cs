using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.AdminArea;
using ProjectAirsoft.ViewModels.Team;
using ProjectAirsoft.ViewModels.User;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class TeamService(ApplicationDbContext dbContext) : BaseService, ITeamService
	{
		public async Task<IEnumerable<TeamIndexViewModel>> GetAllTeamsAsync(string? city = null)
		{
			List<Team> teams = await dbContext.Teams
				.AsNoTracking()
				.Include(t => t.City)
				.Include(t => t.Leader)
				.ToListAsync();

			if (!string.IsNullOrEmpty(city))
			{
				city = city.ToLower().Trim();
				teams = teams
					.Where(t => t.City.Name.ToLower() == city)
					.ToList();
			}

			IEnumerable<TeamIndexViewModel> teamIndexViewModels = teams
				.Where(t => t.IsDeleted == false)
				.Select(t => new TeamIndexViewModel()
				{
					Id = t.Id.ToString(),
					Name = t.Name,
					LogoUrl = t.LogoUrl,
					City = t.City.Name,
					Leader = t.Leader.UserName!
				});

			return teamIndexViewModels;
		}

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

		public async Task<TeamDetailsViewModel> GetTeamDetailsAsync(Guid id)
		{
			Team? team = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.Include(t => t.Leader)
				.FirstOrDefaultAsync(t => t.Id == id);

			TeamDetailsViewModel? viewModel = new TeamDetailsViewModel();

			IEnumerable<UserViewModel> members = await dbContext.Users
				.AsNoTracking()
				.Where(u => u.TeamId == id && u.IsDeleted == false)
				.Select(u => new UserViewModel()
				{
					UserName = u.UserName!
				})
				.ToListAsync();

			if (team != null)
			{
				bool isLeaderDeleted = team.Leader.IsDeleted == true;

				viewModel.Id = team.Id.ToString();
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.City = team.City.Name;
				viewModel.Leader = isLeaderDeleted ? "(Deleted User)" : team.Leader.UserName!;
				viewModel.Members = members;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<TeamJoinViewModel> GetTeamJoinAsync(Guid id)
		{
			Team? team = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.FirstOrDefaultAsync(t => t.Id == id);

			TeamJoinViewModel viewModel = new TeamJoinViewModel();

			if (team != null)
			{
				viewModel.Id = team.Id.ToString();
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.City = team.City.Name;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> JoinTeamAsync(Guid id, ApplicationUser user)
		{
			Team? team = await dbContext.Teams
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (team == null)
			{
				return false;
			}

			user.TeamId = team.Id;
			team.Members.Add(user);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<TeamLeaveViewModel> GetTeamLeaveAsync(Guid id)
		{
			Team? team = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.Include(t => t.Leader)
				.FirstOrDefaultAsync(t => t.Id == id);

			TeamLeaveViewModel viewModel = new TeamLeaveViewModel();

			if (team != null)
			{
				viewModel.Id = team.Id.ToString();
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.City = team.City.Name;
				viewModel.Leader = team.Leader.UserName!;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> LeaveTeamAsync(Guid id, ApplicationUser user)
		{
			Team? team = await dbContext.Teams
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (team == null)
			{
				return false;
			}

			user.TeamId = null;
			team.Members.Remove(user);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> TeamExistsAsync(string id)
		{
			bool result = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.AnyAsync(t => t.Id.ToString() == id);

			return result;
		}

		public async Task<TeamFormModel> GetTeamForEditAsync(string teamId, string userId)
		{
			Team? team = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id.ToString() == teamId);

			TeamFormModel viewModel = new TeamFormModel();

			if (team != null)
			{
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.CityId = team.CityId;
				viewModel.LeaderId = userId;
			}
			else
			{
				viewModel = null!;
			}

			if (team!.LeaderId.ToString() != userId)
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> EditTeamAsync(TeamFormModel viewModel, Guid id)
		{
			Team? team = await dbContext.Teams
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (team == null)
			{
				return false;
			}

			team.Name = viewModel.Name;
			team.LogoUrl = viewModel.LogoUrl;
			team.CityId = viewModel.CityId;

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<TeamDeleteViewModel> GetTeamForDeleteAsync(string id)
		{
			Team? team = await dbContext.Teams
				.AsNoTracking()
				.Where(t => t.IsDeleted == false)
				.Include(t => t.City)
				.Include(t => t.Leader)
				.FirstOrDefaultAsync(t => t.Id.ToString() == id);

			TeamDeleteViewModel viewModel = new TeamDeleteViewModel();

			if (team != null)
			{
				viewModel.Id = team.Id.ToString();
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.City = team.City.Name;
				viewModel.Leader = team.Leader.UserName!;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}

		public async Task<bool> DeleteTeamAsync(Guid id)
		{
			Team? team = await dbContext.Teams
				.Include(t => t.Members)
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (team == null)
			{
				return false;
			}

			team.IsDeleted = true;

			foreach (ApplicationUser member in team.Members)
			{
				member.TeamId = null;
			}

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<TeamViewModel>> GetAllTeamsForAdminAreaAsync()
		{
			List<Team> teams = await dbContext.Teams
				.AsNoTracking()
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

			return teamViewModels;
		}
	}
}
