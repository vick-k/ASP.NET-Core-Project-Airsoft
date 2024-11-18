using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProjectAirsoft.Common.EntityValidationConstants.Game;

namespace ProjectAirsoft.Data.Models
{
	public class Game
	{
		[Key]
		[Comment("The game identifier")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[Comment("The name of the game")]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[Comment("The description of the game")]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Comment("The image URL of the game")]
		[MaxLength(ImageUrlMaxLength)]
		public string? ImageUrl { get; set; }

		[Comment("The date when the game will be held")]
		public DateTime Date { get; set; }

		[Required]
		[Comment("The time when the game will start")]
		[MaxLength(StartTimeMaxLength)]
		public string StartTime { get; set; } = null!;

		[Comment("The maximum number of players")]
		public int Capacity { get; set; }

		[Comment("The one-time fee that players have to pay")]
		public decimal Fee { get; set; }

		[Comment("Flag for soft delete")]
		public bool IsDeleted { get; set; }

		[Comment("Flag for canceled games")]
		public bool IsCanceled { get; set; }

		[ForeignKey(nameof(Terrain))]
		[Comment("The terrain identifier")]
		public Guid TerrainId { get; set; }

		public virtual Terrain Terrain { get; set; } = null!;

		[Comment("The organizer identifier")]
		public Guid OrganizerId { get; set; }

		public virtual ApplicationUser Organizer { get; set; } = null!;

		public virtual ICollection<UserGame> UsersGames { get; set; } = new HashSet<UserGame>();

		public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
	}
}
