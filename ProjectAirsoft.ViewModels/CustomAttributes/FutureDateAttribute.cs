using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.ValidationMessages.FutureDateAttribute;

namespace ProjectAirsoft.ViewModels.CustomAttributes
{
	public class FutureDateAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return new ValidationResult(RequiredDateMessage);
			}

			if (DateTime.TryParse(value.ToString(), out DateTime date))
			{
				if (date < DateTime.Now.Date)
				{
					return new ValidationResult(PastDateMessage);
				}

				return ValidationResult.Success;
			}

			return new ValidationResult(InvalidDateMessage);
		}
	}
}
