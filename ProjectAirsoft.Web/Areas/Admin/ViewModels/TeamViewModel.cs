namespace ProjectAirsoft.Web.Areas.Admin.ViewModels
{
	public class TeamViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string City { get; set; } = null!;

		public string Leader { get; set; } = null!;

		public bool IsDeleted { get; set; }
	}
}
