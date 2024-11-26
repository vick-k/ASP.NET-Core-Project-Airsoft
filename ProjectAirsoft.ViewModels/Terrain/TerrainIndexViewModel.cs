namespace ProjectAirsoft.ViewModels.Terrain
{
	public class TerrainIndexViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string LocationUrl { get; set; } = null!;

		public string City { get; set; } = null!;

		public int GamesCount { get; set; }

		public bool IsDeleted { get; set; }

		public bool IsActive { get; set; }
	}
}
