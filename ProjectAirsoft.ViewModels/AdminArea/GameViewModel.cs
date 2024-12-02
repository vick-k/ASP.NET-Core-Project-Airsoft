namespace ProjectAirsoft.ViewModels.AdminArea
{
	public class GameViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string Date { get; set; } = null!;

		public int Capacity { get; set; }

		public int RegisteredPlayers { get; set; }

		public decimal Fee { get; set; }

		public string Terrain { get; set; } = null!;

		public string Organizer { get; set; } = null!;

		public bool IsDeleted { get; set; }
	}
}
