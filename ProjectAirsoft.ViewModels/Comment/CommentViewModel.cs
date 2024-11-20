namespace ProjectAirsoft.ViewModels.Comment
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Author { get; set; } = null!;

		public string CreatedOn { get; set; } = null!;

		public string Content { get; set; } = null!;

		public bool IsDeleted { get; set; }
	}
}
