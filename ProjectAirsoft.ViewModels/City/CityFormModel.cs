using System.ComponentModel.DataAnnotations;
using static ProjectAirsoft.Common.EntityValidationConstants.City;
using static ProjectAirsoft.Common.ValidationMessages.City;

namespace ProjectAirsoft.ViewModels.City
{
	public class CityFormModel
	{
		[Required(ErrorMessage = RequiredNameMessage)]
		[MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
		[MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
		public string Name { get; set; } = null!;
	}
}
