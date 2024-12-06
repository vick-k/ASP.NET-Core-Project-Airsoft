namespace ProjectAirsoft.Common
{
	public static class AlertMessages
	{
		public static class Game
		{
			public const string AddGameFailMessage = "Some error appears when trying to add a new game.";
			public const string AddGameSuccessMessage = "The game has been added successfully.";
			public const string EditGameNotOwnerMessage = "You cannot edit someone else's game.";
			public const string EditGameGenericMessage = "Some error appears when trying to edit the game.";
			public const string GameDoesNotExistMessage = "The game doesn't exist anymore.";
			public const string EditGameSuccessMessage = "The game has been edited successfully.";
			public const string GameGenericErrorMessage = "An unexpected error appears.";
			public const string GameCancelSuccessMessage = "The game has been canceled.";
			public const string DeleteGameNowOwnerMessage = "You cannot delete someone else's game.";
			public const string DeleteGameSuccessMessage = "The game has been deleted.";
		}

		public static class Comment
		{
			public const string AddCommentFailMessage = "Some error appears when trying to add a new comment.";
			public const string CommentDoesNotExistMessage = "The comment you are trying to delete doesn't exist anymore.";
			public const string CommentNotOwnerMessage = "You cannot delete someone else's comment.";
			public const string CommentGenericErrorMessage = "Some error appears when trying to delete a comment.";
		}

		public static class GameList
		{
			public const string GameRegisterFailMessage = "Some error appears when trying to register for a game.";
			public const string GameRegisterFullMessage = "The game is full.";
			public const string GameRegisterSuccessMessage = "You have been successfully registered for the game.";
			public const string GameUnregisterFailMessage = "Some error appears when trying to unregister for a game.";
			public const string GameUnregisterSuccessMessage = "You have been successfully unregistered from the game.";
		}
	}
}
