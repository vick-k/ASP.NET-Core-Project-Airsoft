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
			public const string DeleteGameFailMessage = "Some error appears when trying to delete a game.";
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

		public static class City
		{
			public const string CityDoesNotExistMessage = "The city you are trying to edit doesn't exists.";
			public const string CityEditFailMessage = "Some error appears when trying to edit the city.";
			public const string CityEditSuccessMessage = "The city has been edited successfully.";
			public const string CityAlreadyExistMessage = "One or more cities you are trying to add already exist.";
		}

		public static class Terrain
		{
			public const string TerrainDuplicateMessage = "There is already a terrain with the same name and city.";
			public const string AddTerrainSuccessMessage = "The terrain has been added successfully.";
			public const string TerrainDoesNotExistMessage = "The terrain you are trying to edit doesn't exists.";
			public const string EditTerrainFailMessage = "Some error appears when trying to edit the terrain.";
			public const string EditTerrainSuccessMessage = "The terrain has been edited successfully.";
			public const string TerrainChangeStatusFailMessage = "Some error appears when trying to change the status of the terrain.";
			public const string TerrainChangeStatusSuccessMessage = "The terrain status has been successfully changed.";
			public const string DeleteTerrainFailMessage = "Some error appears when trying to delete the terrain.";
			public const string DeleteTerrainSuccessMessage = "The terrain has been deleted.";
		}

		public static class User
		{
			public const string UserDoesNotExistMessage = "The user doesn't exists.";
			public const string UserAssignRoleFailMessage = "Some error appears when trying to assign a role to the user.";
			public const string UserAssignRoleSuccessMessage = "The role has been assigned successfully to the user.";
			public const string UserRemoveRoleFailMessage = "Some error appears when trying to remove a role from the user.";
			public const string UserRemoveRoleSuccessMessage = "The role has been removed from the user.";
			public const string AdminDeleteHimselfErrorMessage = "You cannot delete yourself.";
			public const string UserDeleteFailMessage = "Some error appears when trying to delete the user.";
			public const string UserDeleteSuccessMessage = "The user has been deleted.";
		}
	}
}
