using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProjectAirsoft.Common.EntityValidationConstants.Comment;

namespace ProjectAirsoft.Data.Models
{
	public class Comment
	{
		[Key]
		[Comment("The comment identifier")]
		public int Id { get; set; }

		[Required]
		[Comment("The content of the comment")]
		[MaxLength(ContentMaxLength)]
		public string Content { get; set; } = null!;

		[Comment("The date and time when the comment is created")]
		public DateTime CreatedOn { get; set; }

		[ForeignKey(nameof(Author))]
		[Comment("The author identifier")]
		public Guid AuthorId { get; set; }

		public virtual ApplicationUser Author { get; set; } = null!;

		[ForeignKey(nameof(Game))]
		[Comment("The game identifier")]
		public Guid GameId { get; set; }

		public virtual Game Game { get; set; } = null!;
	}
}
