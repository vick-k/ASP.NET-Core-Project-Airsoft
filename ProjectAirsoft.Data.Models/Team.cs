using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProjectAirsoft.Common.EntityValidationConstants.Team;

namespace ProjectAirsoft.Data.Models
{
	public class Team
	{
		[Key]
		[Comment("The team identifier")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[Comment("The name of the team")]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[Comment("The logo URL of the game")]
		[MaxLength(LogoUrlMaxLength)]
		public string LogoUrl { get; set; } = null!;

		[ForeignKey(nameof(City))]
		[Comment("The city identifier")]
		public int CityId { get; set; }

		public virtual City City { get; set; } = null!;

		[ForeignKey(nameof(Leader))]
		[Comment("The leader identifier")]
		public Guid LeaderId { get; set; }

		public virtual ApplicationUser Leader { get; set; } = null!;

		public bool IsDeleted { get; set; }

		public virtual ICollection<ApplicationUser> Members { get; set; } = new HashSet<ApplicationUser>();
	}
}
