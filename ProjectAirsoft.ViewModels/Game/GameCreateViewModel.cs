using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.ViewModels.Game
{
	public class GameCreateViewModel // TODO: Add validations
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string? ImageUrl { get; set; }

		public string Date { get; set; } = null!;

		public int Capacity { get; set; }

		public decimal Fee { get; set; }

		public string OrganizerId { get; set; } = null!;

		public string TerrainId { get; set; } = null!;

		public IEnumerable<TerrainViewModel>? Terrains { get; set; }
	}
}
