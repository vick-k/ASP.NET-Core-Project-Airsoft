namespace ProjectAirsoft.Common
{
	public static class ValidationMessages
	{
		public static class Game
		{
			public const string RequiredNameMessage = "The name is required.";
			public const string NameMinLengthMessage = "The name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The name length cannot be more than {1} characters.";

			public const string RequiredDescriptionMessage = "The description is required.";
			public const string DescriptionMinLengthMessage = "The description length must be at least {1} characters.";
			public const string DescriptionMaxLengthMessage = "The description length cannot be more than {1} characters.";

			public const string ImageUrlMaxLengthMessage = "The image link length cannot be more than {1} characters.";

			public const string RequiredDateMessage = "The date is required.";

			public const string RequiredStartTimeMessage = "The start time is required.";
			public const string InvalidStartTimeMessage = "The start time must be in the following format: HH:mm";

			public const string RequiredCapacityMessage = "The capacity is required.";
			public const string CapacityRangeMessage = "The capacity must be between {1} and {2}.";
			public const string CapacityLessThanRegisteredPlayers = "The capacity cannot be less than the current registered players.";

			public const string RequiredFeeMessage = "The fee is required.";
			public const string FeeRangeMessage = "The fee must be between {1} and {2}.";

			public const string RequiredTerrainMessage = "The terrain is required.";
		}

		public static class Terrain
		{
			public const string RequiredNameMessage = "The name is required.";
			public const string NameMinLengthMessage = "The name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The name length cannot be more than {1} characters.";

			public const string RequiredLocationMessage = "The location is required.";
			public const string LocationUrlMaxLengthMessage = "The location link length cannot be more than {1} characters.";

			public const string RequiredCityMessage = "The city is required.";
		}

		public static class Comment
		{
			public const string RequiredContentMessage = "The comment cannot be empty.";
			public const string ContentMinLengthMessage = "The comment length must be at least {1} characters.";
			public const string ContentMaxLengthMessage = "The comment length cannot be more than {1} characters.";
		}

		public static class Team
		{
			public const string RequiredNameMessage = "The name is required.";
			public const string NameMinLengthMessage = "The name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The name length cannot be more than {1} characters.";

			public const string RequiredLogoMessage = "The logo is required.";
			public const string LogoUrlMaxLengthMessage = "The logo link length cannot be more than {1} characters.";

			public const string RequiredCityMessage = "The city is required.";
		}

		public static class FutureDateAttribute
		{
			public const string RequiredDateMessage = "The date is required.";
			public const string PastDateMessage = "The selected date cannot be in the past.";
			public const string InvalidDateMessage = "Invalid date format.";
		}
	}
}
