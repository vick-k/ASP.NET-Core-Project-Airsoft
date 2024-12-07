namespace ProjectAirsoft.ViewModels.Game
{
	public class AllGamesFilterViewModel
	{
		public IEnumerable<GameIndexViewModel>? Games { get; set; }

		public string? TerrainFilter { get; set; }

		public IEnumerable<string>? AllTerrains { get; set; }

		public int? CurrentPage { get; set; } = 1;

		public int? EntitiesPerPage { get; set; } = 6;

		public int? TotalPages { get; set; }
	}
}
