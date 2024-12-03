using ProjectAirsoft.ViewModels.CustomAttributes;
using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.ValidationMessages.City;

namespace ProjectAirsoft.ViewModels.City
{
	public class CityInputViewModel
	{
		[Required(ErrorMessage = RequiredNamesMessage)]
		[CityLineValidation]
		public string CityNames { get; set; } = null!;
	}
}
