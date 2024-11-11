namespace ProjectAirsoft.ViewModels.Game
{
	public class GameIndexViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string? ImageUrl { get; set; }

		public string Date { get; set; } = null!;

		public string Terrain { get; set; } = null!;

		public bool IsCanceled { get; set; }
	}
}
