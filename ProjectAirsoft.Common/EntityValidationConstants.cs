﻿namespace ProjectAirsoft.Common
{
	public static class EntityValidationConstants
	{
		public static class Game
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 80;

			public const int DescriptionMinLength = 10;
			public const int DescriptionMaxLength = 2000;

			public const int ImageUrlMaxLength = 2048;

			public const int StartTimeMaxLength = 5;
			public const string StartTimeRegexPattern = @"^\d{2}:\d{2}$";

			public const int CapacityMinValue = 1;
			public const int CapacityMaxValue = 5000;

			public const string FeeMinValue = "0";
			public const string FeeMaxValue = "1000";
		}

		public static class Terrain
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 50;

			public const int LocationUrlMaxLength = 2048;
		}

		public static class City
		{
			public const int NameMinLength = 2;
			public const int NameMaxLength = 80;
		}

		public static class Comment
		{
			public const int ContentMinLength = 1;
			public const int ContentMaxLength = 500;
		}

		public static class Team
		{
			public const int NameMinLength = 2;
			public const int NameMaxLength = 60;

			public const int LogoUrlMaxLength = 2048;
		}
	}
}
