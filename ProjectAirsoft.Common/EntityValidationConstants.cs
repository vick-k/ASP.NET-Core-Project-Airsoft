namespace ProjectAirsoft.Common
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
	}
}
