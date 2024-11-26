﻿namespace ProjectAirsoft.Common
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
		}

		public static class Terrain
		{
			public const string RequiredNameMessage = "The Name is required.";
			public const string NameMinLengthMessage = "The Name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The Name length cannot be more than {1} characters.";

			public const string RequiredLocationMessage = "The Location is required.";
			public const string LocationUrlMaxLengthMessage = "The Location Link length cannot be more than {1} characters.";

			public const string RequiredCityMessage = "The City is required.";
		}

		public static class Comment
		{
			public const string RequiredContentMessage = "The Comment cannot be empty.";
			public const string ContentMinLengthMessage = "The Comment length must be at least {1} characters.";
			public const string ContentMaxLengthMessage = "The Comment length cannot be more than {1} characters.";
		}

		public static class Team
		{
			public const string RequiredNameMessage = "The Name is required.";
			public const string NameMinLengthMessage = "The Name length must be at least {1} characters.";
			public const string NameMaxLengthMessage = "The Name length cannot be more than {1} characters.";

			public const string RequiredLogoMessage = "The Logo is required.";
			public const string LogoUrlMaxLengthMessage = "The Logo Link length cannot be more than {1} characters.";

			public const string RequiredCityMessage = "The City is required.";
		}

		public static class FutureDateAttribute
		{
			public const string RequiredDateMessage = "The Date is required.";
			public const string PastDateMessage = "The selected date cannot be in the past.";
			public const string InvalidDateMessage = "Invalid date format.";
		}
	}
}
