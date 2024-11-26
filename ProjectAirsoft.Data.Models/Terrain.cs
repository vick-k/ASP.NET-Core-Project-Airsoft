using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProjectAirsoft.Common.EntityValidationConstants.Terrain;

namespace ProjectAirsoft.Data.Models
{
	public class Terrain
	{
		[Key]
		[Comment("The terrain identifier")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[Comment("The name of the terrain")]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[Comment("The URL of the location")]
		[MaxLength(LocationUrlMaxLength)]
		public string LocationUrl { get; set; } = null!;

		[Comment("Flag for soft delete")]
		public bool IsDeleted { get; set; }

		[Comment("Flag for active/inactive terrain")]
		public bool IsActive { get; set; }

		[ForeignKey(nameof(City))]
		[Comment("The city identifier")]
		public int CityId { get; set; }

		public virtual City City { get; set; } = null!;

		public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
	}
}
