using ProjectAirsoft.ViewModels.User;

namespace ProjectAirsoft.ViewModels.Team
{
	public class TeamDetailsViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string LogoUrl { get; set; } = null!;

		public string City { get; set; } = null!;

		public string Leader { get; set; } = null!;

		public IEnumerable<UserViewModel>? Members { get; set; }
	}
}
