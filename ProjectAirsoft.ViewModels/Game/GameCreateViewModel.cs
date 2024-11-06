using ProjectAirsoft.ViewModels.Terrain;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.Game;

namespace ProjectAirsoft.ViewModels.Game
{
	public class GameCreateViewModel
	{
		[Required(ErrorMessage = "The Name is required.")]
		[MinLength(NameMinLength, ErrorMessage = "The Name length must be at least {1} characters.")]
		[MaxLength(NameMaxLength, ErrorMessage = "The Name length cannot be more than {1} characters.")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "The Description is required.")]
		[MinLength(DescriptionMinLength, ErrorMessage = "The Description length must be at least {1} characters.")]
		[MaxLength(DescriptionMaxLength, ErrorMessage = "The Description length cannot be more than {1} characters.")]
		public string Description { get; set; } = null!;

		[MaxLength(ImageUrlMaxLength, ErrorMessage = "The Image Link length cannot be more than {1} characters.")]
		public string? ImageUrl { get; set; }

		[Required(ErrorMessage = "The Date is required.")]
		public string Date { get; set; } = null!;

		[Required(ErrorMessage = "The Capacity is required.")]
		[Range(CapacityMinValue, CapacityMaxValue, ErrorMessage = "The Capacity must be between {1} and {2}.")]
		public int Capacity { get; set; }

		[Required(ErrorMessage = "The Fee is required.")]
		[Range(typeof(decimal), FeeMinValue, FeeMaxValue, ErrorMessage = "The Fee must be between {1} and {2}.")]
		public decimal Fee { get; set; }

		public string? OrganizerId { get; set; }

		[Required(ErrorMessage = "The Terrain is required.")]
		public string TerrainId { get; set; } = null!;

		public IEnumerable<TerrainViewModel>? Terrains { get; set; }
	}
}
