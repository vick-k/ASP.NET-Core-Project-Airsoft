using ProjectAirsoft.Data.Models;
using ProjectAirsoft.ViewModels.AdminArea;

namespace ProjectAirsoft.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserIndexViewModel>> GetAllUsersAsync();

		Task<bool> UserExistsAsync(string id);

		Task<ApplicationUser?> GetUserAsync(string username);

		Task<bool> AssignUserToRoleAsync(string userId, string role);

		Task<bool> RemoveUserRoleAsync(string userId, string role);

		Task<bool> DeleteUserAsync(string id);
	}
}
