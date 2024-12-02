namespace ProjectAirsoft.Web.Areas.Admin.ViewModels
{
	public class TerrainViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string Location { get; set; } = null!;

		public string City { get; set; } = null!;

		public bool IsActive { get; set; }

		public bool IsDeleted { get; set; }
	}
}
