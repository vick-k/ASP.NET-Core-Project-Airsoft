using ProjectAirsoft.ViewModels.Terrain;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.Game;
using static ProjectAirsoft.Common.ValidationMessages.Game;

namespace ProjectAirsoft.ViewModels.Game
{
	public class GameCreateViewModel
	{
		[Required(ErrorMessage = RequiredNameMessage)]
		[MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
		[MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = RequiredDescriptionMessage)]
		[MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
		[MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
		public string Description { get; set; } = null!;

		[MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
		public string? ImageUrl { get; set; }

		[Required(ErrorMessage = RequiredDateMessage)]
		public string Date { get; set; } = null!;

		[Required(ErrorMessage = RequiredCapacityMessage)]
		[Range(CapacityMinValue, CapacityMaxValue, ErrorMessage = CapacityRangeMessage)]
		public int Capacity { get; set; }

		[Required(ErrorMessage = RequiredFeeMessage)]
		[Range(typeof(decimal), FeeMinValue, FeeMaxValue, ErrorMessage = FeeRangeMessage)]
		public decimal Fee { get; set; }

		public string? OrganizerId { get; set; }

		[Required(ErrorMessage = RequiredTerrainMessage)]
		public string TerrainId { get; set; } = null!;

		public IEnumerable<TerrainViewModel>? Terrains { get; set; }
	}
}
