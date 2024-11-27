using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.ValidationMessages.ValidTimeAttribute;

namespace ProjectAirsoft.Infrastructure.CustomAttributes
{
	public class ValidTimeAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return new ValidationResult(RequiredStartTimeMessage);
			}

			if (TimeSpan.TryParse(value.ToString(), out TimeSpan _))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(InvalidStartTimeMessage);
		}
	}
}
