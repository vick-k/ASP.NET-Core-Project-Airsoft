using ProjectAirsoft.ViewModels.User;

namespace ProjectAirsoft.ViewModels.Game
{
	public class GameRegisteredPlayersViewModel
	{
		public string Name { get; set; } = null!;

		public string Date { get; set; } = null!;

		public IEnumerable<UserViewModel>? RegisteredPlayers { get; set; }
	}
}
