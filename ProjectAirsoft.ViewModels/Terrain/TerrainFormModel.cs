using ProjectAirsoft.ViewModels.City;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.Terrain;
using static ProjectAirsoft.Common.ValidationMessages.Terrain;

namespace ProjectAirsoft.ViewModels.Terrain
{
	public class TerrainFormModel
	{
		[Required(ErrorMessage = RequiredNameMessage)]
		[MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
		[MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = RequiredLocationMessage)]
		[MaxLength(LocationUrlMaxLength, ErrorMessage = LocationUrlMaxLengthMessage)]
		public string LocationUrl { get; set; } = null!;

		[Required(ErrorMessage = RequiredCityMessage)]
		public int CityId { get; set; }

		public IEnumerable<CityListModel>? Cities { get; set; }
	}
}
