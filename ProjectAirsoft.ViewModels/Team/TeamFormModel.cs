using ProjectAirsoft.ViewModels.City;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.Team;
using static ProjectAirsoft.Common.ValidationMessages.Team;

namespace ProjectAirsoft.ViewModels.Team
{
	public class TeamFormModel
	{
		[Required(ErrorMessage = RequiredNameMessage)]
		[MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
		[MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = RequiredLogoMessage)]
		[MaxLength(LogoUrlMaxLength, ErrorMessage = LogoUrlMaxLengthMessage)]
		public string LogoUrl { get; set; } = null!;

		[Required(ErrorMessage = RequiredCityMessage)]
		public int CityId { get; set; }

		public IEnumerable<CityListModel>? Cities { get; set; }
	}
}
