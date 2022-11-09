public class GameCenterBinding
{
	public static bool isGameCenterAvailable()
	{
		return false;
	}

	public static void authenticateLocalPlayer()
	{
	}

	public static bool isPlayerAuthenticated()
	{
		return false;
	}

	public static string playerAlias()
	{
		return string.Empty;
	}

	public static string playerIdentifier()
	{
		return string.Empty;
	}

	public static bool isUnderage()
	{
		return false;
	}

	public static void retrieveFriends(bool loadProfileImages, bool loadLargeProfileImages = true)
	{
	}

	public static void loadPlayerData(string[] playerIdArray, bool loadProfileImages, bool loadLargeProfileImages = true)
	{
	}

	public static void loadProfilePhotoForLocalPlayer()
	{
	}

	public static void loadLeaderboardTitles()
	{
	}

	public static void reportScore(long score, string leaderboardId)
	{
	}

	public static void reportScore(long score, long context, string leaderboardId)
	{
	}

	public static void issueScoreChallenge(long score, long context, string leaderboardId, string[] playerIds, string message)
	{
	}

	public static void showLeaderboardWithTimeScope(GameCenterLeaderboardTimeScope timeScope)
	{
	}

	public static void showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope timeScope, string leaderboardId)
	{
	}

	public static void retrieveScores(bool friendsOnly, GameCenterLeaderboardTimeScope timeScope, int start, int end)
	{
	}

	public static void retrieveScores(bool friendsOnly, GameCenterLeaderboardTimeScope timeScope, int start, int end, string leaderboardId)
	{
	}

	public static void retrieveScoresForPlayerId(string playerId)
	{
	}

	public static void retrieveScoresForPlayerId(string playerId, string leaderboardId)
	{
	}

	public static void reportAchievement(string identifier, float percent)
	{
	}

	public static void selectChallengeablePlayerIDsForAchievement(string identifier, string[] playerIds)
	{
	}

	public static void issueAchievementChallenge(string identifier, string[] playerIds, string message)
	{
	}

	public static void getAchievements()
	{
	}

	public static void resetAchievements()
	{
	}

	public static void showAchievements()
	{
	}

	public static void retrieveAchievementMetadata()
	{
	}

	public static void showCompletionBannerForAchievements()
	{
	}
}
