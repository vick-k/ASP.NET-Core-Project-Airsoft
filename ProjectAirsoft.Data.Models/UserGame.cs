using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAirsoft.Data.Models
{
	public class UserGame
	{
		[ForeignKey(nameof(User))]
		[Comment("The user identifier")]
		public Guid UserId { get; set; }

		public virtual ApplicationUser User { get; set; } = null!;

		[ForeignKey(nameof(Game))]
		[Comment("The game identifier")]
		public Guid GameId { get; set; }

		public virtual Game Game { get; set; } = null!;
	}
}
