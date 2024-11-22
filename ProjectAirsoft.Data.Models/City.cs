using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.City;

namespace ProjectAirsoft.Data.Models
{
	public class City
	{
		[Key]
		[Comment("The city identifier")]
		public int Id { get; set; }

		[Required]
		[Comment("The name of the city")]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Terrain> Terrains { get; set; } = new HashSet<Terrain>();

		public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
	}
}
