using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.City;
using static ProjectAirsoft.Common.ValidationMessages.City;

namespace ProjectAirsoft.ViewModels.CustomAttributes
{
	public class CityLineValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return new ValidationResult(RequiredNameMessage);
			}

			string[]? cityNames = value.ToString()?.Split(Environment.NewLine);

			if (cityNames == null || cityNames.Length == 0)
			{
				return new ValidationResult(RequiredNamesMessage);
			}

			foreach (string city in cityNames)
			{
				string trimmedCity = city.Trim();

				if (string.IsNullOrEmpty(trimmedCity))
				{
					return new ValidationResult(EmptyLineMessage);
				}

				if (trimmedCity.Length < NameMinLength)
				{
					return new ValidationResult($"The city name length must be at least {NameMinLength} characters.");
				}

				if (trimmedCity.Length > NameMaxLength)
				{
					return new ValidationResult($"The city name length cannot be more than {NameMaxLength} characters.");
				}
			}

			return ValidationResult.Success;
		}
	}
}
