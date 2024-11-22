﻿using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Team;
using ProjectAirsoft.ViewModels.User;
using ProjectAirsoft.Web.Data;

namespace ProjectAirsoft.Services.Data
{
	public class TeamService(ApplicationDbContext dbContext) : BaseService, ITeamService
	{
		public async Task<IEnumerable<TeamIndexViewModel>> GetAllTeamsAsync()
		{
			List<Team> teams = await dbContext.Teams
				.AsNoTracking()
				.Include(t => t.City)
				.ToListAsync();

			IEnumerable<TeamIndexViewModel> teamIndexViewModels = teams
				.Select(t => new TeamIndexViewModel()
				{
					Id = t.Id.ToString(),
					Name = t.Name,
					LogoUrl = t.LogoUrl,
					City = t.City.Name
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
				.Include(t => t.City)
				.Include(t => t.Leader)
				.FirstOrDefaultAsync(t => t.Id == id);

			TeamDetailsViewModel? viewModel = new TeamDetailsViewModel();

			IEnumerable<UserViewModel> members = await dbContext.Users
				.AsNoTracking()
				.Where(u => u.TeamId == id)
				.Select(u => new UserViewModel()
				{
					UserName = u.UserName!
				})
				.ToListAsync();

			if (team != null)
			{
				viewModel.Id = team.Id.ToString();
				viewModel.Name = team.Name;
				viewModel.LogoUrl = team.LogoUrl;
				viewModel.City = team.City.Name;
				viewModel.Leader = team.Leader.UserName!;
				viewModel.Members = members;
			}
			else
			{
				viewModel = null!;
			}

			return viewModel;
		}
	}
}
