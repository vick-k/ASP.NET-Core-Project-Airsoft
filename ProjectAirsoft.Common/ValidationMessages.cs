namespace ProjectAirsoft.Common
{
	public static class ValidationMessages
	{
		public static class Game
		{
			public const string RequiredNameMessage = "The Name is required.";
			public const string NameMinLengthMessage = "The Name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The Name length cannot be more than {1} characters.";

			public const string RequiredDescriptionMessage = "The Description is required.";
			public const string DescriptionMinLengthMessage = "The Description length must be at least {1} characters.";
			public const string DescriptionMaxLengthMessage = "The Description length cannot be more than {1} characters.";

			public const string ImageUrlMaxLengthMessage = "The Image Link length cannot be more than {1} characters.";

			public const string RequiredDateMessage = "The Date is required.";

			public const string RequiredStartTimeMessage = "The Start Time is required.";
			public const string InvalidStartTimeMessage = "The Start Time must be in the following format: HH:mm";

			public const string RequiredCapacityMessage = "The Capacity is required.";
			public const string CapacityRangeMessage = "The Capacity must be between {1} and {2}.";
			public const string CapacityLessThanRegisteredPlayers = "The Capacity cannot be less than the current registered players.";

			public const string RequiredFeeMessage = "The Fee is required.";
			public const string FeeRangeMessage = "The Fee must be between {1} and {2}.";

			public const string RequiredTerrainMessage = "The Terrain is required.";

			public const string InvalidDateMessage = "Please use the built-in calendar to pick a date."; // TODO: not used anymore, can be removed
		}
	}
}
