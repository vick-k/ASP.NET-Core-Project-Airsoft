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
			public const string GameDoesNotExistMessage = "The game doesn't exists anymore.";
			public const string EditGameSuccessMessage = "The game has been edited successfully.";
			public const string GameGenericErrorMessage = "An unexpected error appears.";
			public const string GameCancelSuccessMessage = "The game has been canceled.";
			public const string DeleteGameNowOwnerMessage = "You cannot delete someone else's game.";
			public const string DeleteGameSuccessMessage = "The game has been deleted.";
		}

		public static class Comment
		{
			public const string AddCommentFailMessage = "Some error appears when trying to add a new comment.";
			public const string CommentDoesNotExistMessage = "The comment you are trying to delete doesn't exists anymore.";
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

		public static class Team
		{
			public const string TeamGenericErrorMessage = "An unexpected error appears.";
			public const string AddTeamFailMessage = "Some error appears when trying to create a new team.";
			public const string AddTeamSuccessMessage = "The team has been created successfully.";
			public const string JoinTeamAlreadyInTeamMessage = "You are already a member of another team.";
			public const string JoinTeamFailMessage = "Some error appears when trying to join in a team.";
			public const string JoinTeamSuccessMessage = "You have successfully joined the team.";
			public const string LeaveTeamNotInTeamMessage = "You are not a member of any team.";
			public const string LeaveTeamFailMessage = "Some error appears when trying to leave a team.";
			public const string LeaveTeamSuccessMessage = "You have successfully left the team.";
			public const string EditTeamFailMessage = "Some error appears when trying to edit a team.";
			public const string TeamDoesNotExistMessage = "The team you are trying to edit doesn't exists.";
			public const string EditTeamSuccessMessage = "The team has been edited successfully.";
			public const string EditTeamNotOwnerMessage = "You cannot edit someone else's team.";
			public const string DeleteTeamFailMessage = "Some error appears when trying to delete a team.";
			public const string DeleteTeamSuccessMessage = "The team has been deleted.";
			public const string DeleteTeamNotOwnerMessage = "You cannot delete some else's team.";
		}
	}
}
