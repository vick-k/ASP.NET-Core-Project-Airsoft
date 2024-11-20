using ProjectAirsoft.ViewModels.Comment;

namespace ProjectAirsoft.ViewModels.Game
{
	public class GameDetailsViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public string Date { get; set; } = null!;

		public string StartTime { get; set; } = null!;

		public int RegisteredPlayers { get; set; }

		public int Capacity { get; set; }

		public decimal Fee { get; set; }

		public string Terrain { get; set; } = null!;

		public string Organizer { get; set; } = null!;

		public bool IsUserRegistered { get; set; }

		public bool IsCanceled { get; set; }

		public IEnumerable<CommentViewModel> Comments { get; set; } = new HashSet<CommentViewModel>();

		public CommentFormModel CommentForm { get; set; } = null!;
	}
}
