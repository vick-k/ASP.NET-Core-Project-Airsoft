namespace ProjectAirsoft.ViewModels.Comment
{
	public class CommentDeleteViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; } = null!;

		public string CreatedOn { get; set; } = null!;

		public string Game { get; set; } = null!;

		public string GameId { get; set; } = null!;

		public string AuthorId { get; set; } = null!;
	}
}
