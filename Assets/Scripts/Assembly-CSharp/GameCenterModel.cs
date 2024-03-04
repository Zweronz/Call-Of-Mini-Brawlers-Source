using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameCenterModel
{
	public class FriendScore
	{
		public GameCenterPlayer friend;

		public GameCenterScore score;

		public FriendScore()
		{
		}

		public FriendScore(GameCenterPlayer friend, GameCenterScore score)
		{
			this.friend = friend;
			this.score = score;
		}
	}

	public class EventListener
	{
		public static Action<string> loadPlayerDataFailed;

		public static Action<List<GameCenterPlayer>> playerDataLoaded;

		public static Action playerAuthenticated;

		public static Action<string> playerFailedToAuthenticate;

		public static Action playerLoggedOut;

		public static Action<string> profilePhotoLoaded;

		public static Action<string> profilePhotoFailed;

		public static Action<string> loadCategoryTitlesFailed;

		public static Action<List<GameCenterLeaderboard>> categoriesLoaded;

		public static Action<string> reportScoreFailed;

		public static Action<string> reportScoreFinished;

		public static Action<string> retrieveScoresFailed;

		public static Action<List<GameCenterScore>> scoresLoaded;

		public static Action<string> retrieveScoresForPlayerIdFailed;

		public static Action<List<GameCenterScore>> scoresForPlayerIdLoaded;

		public static Action<string> reportAchievementFailed;

		public static Action<string> reportAchievementFinished;

		public static Action<string> loadAchievementsFailed;

		public static Action<List<GameCenterAchievement>> achievementsLoaded;

		public static Action<string> resetAchievementsFailed;

		public static Action resetAchievementsFinished;

		public static Action<string> retrieveAchievementMetadataFailed;

		public static Action<List<GameCenterAchievementMetadata>> achievementMetadataLoaded;

		public static Action<GameCenterChallenge> localPlayerDidSelectChallengeEvent;

		public static Action<GameCenterChallenge> localPlayerDidCompleteChallengeEvent;

		public static Action<GameCenterChallenge> remotePlayerDidCompleteChallengeEvent;

		private static bool isInitialized;

		public static void Initialize()
		{
			if (isInitialized)
			{
				return;
			}
			GameCenterManager.loadPlayerDataFailed += delegate(string message)
			{
				if (loadPlayerDataFailed != null)
				{
					loadPlayerDataFailed(message);
				}
			};
			GameCenterManager.playerDataLoaded += delegate(List<GameCenterPlayer> playerData)
			{
				if (playerDataLoaded != null)
				{
					playerDataLoaded(playerData);
				}
			};
			GameCenterManager.playerAuthenticated += delegate
			{
				if (playerAuthenticated != null)
				{
					playerAuthenticated();
				}
			};
			GameCenterManager.playerFailedToAuthenticate += delegate(string error)
			{
				if (playerFailedToAuthenticate != null)
				{
					playerFailedToAuthenticate(error);
				}
			};
			GameCenterManager.playerLoggedOut += delegate
			{
				if (playerLoggedOut != null)
				{
					playerLoggedOut();
				}
			};
			GameCenterManager.profilePhotoLoaded += delegate(string path)
			{
				if (profilePhotoLoaded != null)
				{
					profilePhotoLoaded(path);
				}
			};
			GameCenterManager.profilePhotoFailed += delegate(string error)
			{
				if (profilePhotoFailed != null)
				{
					profilePhotoFailed(error);
				}
			};
			GameCenterManager.loadCategoryTitlesFailed += delegate(string error)
			{
				if (loadCategoryTitlesFailed != null)
				{
					loadCategoryTitlesFailed(error);
				}
			};
			GameCenterManager.categoriesLoaded += delegate(List<GameCenterLeaderboard> leaderboardList)
			{
				if (categoriesLoaded != null)
				{
					categoriesLoaded(leaderboardList);
				}
			};
			GameCenterManager.reportScoreFailed += delegate(string error)
			{
				if (reportScoreFailed != null)
				{
					reportScoreFailed(error);
				}
			};
			GameCenterManager.reportScoreFinished += delegate(string category)
			{
				if (reportScoreFinished != null)
				{
					reportScoreFinished(category);
				}
			};
			GameCenterManager.retrieveScoresFailed += delegate(string error)
			{
				if (retrieveScoresFailed != null)
				{
					retrieveScoresFailed(error);
				}
			};
			GameCenterManager.scoresLoaded += delegate(List<GameCenterScore> scoreList)
			{
				if (scoresLoaded != null)
				{
					scoresLoaded(scoreList);
				}
			};
			GameCenterManager.retrieveScoresForPlayerIdFailed += delegate(string error)
			{
				if (retrieveScoresForPlayerIdFailed != null)
				{
					retrieveScoresForPlayerIdFailed(error);
				}
			};
			GameCenterManager.scoresForPlayerIdLoaded += delegate(List<GameCenterScore> scoreList)
			{
				if (scoresForPlayerIdLoaded != null)
				{
					scoresForPlayerIdLoaded(scoreList);
				}
			};
			GameCenterManager.reportAchievementFailed += delegate(string error)
			{
				if (reportAchievementFailed != null)
				{
					reportAchievementFailed(error);
				}
			};
			GameCenterManager.reportAchievementFinished += delegate(string identifier)
			{
				if (reportAchievementFinished != null)
				{
					reportAchievementFinished(identifier);
				}
			};
			GameCenterManager.loadAchievementsFailed += delegate(string error)
			{
				if (loadAchievementsFailed != null)
				{
					loadAchievementsFailed(error);
				}
			};
			GameCenterManager.achievementsLoaded += delegate(List<GameCenterAchievement> achievementList)
			{
				if (achievementsLoaded != null)
				{
					achievementsLoaded(achievementList);
				}
			};
			GameCenterManager.resetAchievementsFailed += delegate(string error)
			{
				if (resetAchievementsFailed != null)
				{
					resetAchievementsFailed(error);
				}
			};
			GameCenterManager.resetAchievementsFinished += delegate
			{
				if (resetAchievementsFinished != null)
				{
					resetAchievementsFinished();
				}
			};
			GameCenterManager.retrieveAchievementMetadataFailed += delegate(string error)
			{
				if (retrieveAchievementMetadataFailed != null)
				{
					retrieveAchievementMetadataFailed(error);
				}
			};
			GameCenterManager.achievementMetadataLoaded += delegate(List<GameCenterAchievementMetadata> achievementMetadataList)
			{
				if (achievementMetadataLoaded != null)
				{
					achievementMetadataLoaded(achievementMetadataList);
				}
			};
			GameCenterManager.localPlayerDidSelectChallengeEvent += delegate(GameCenterChallenge challenge)
			{
				if (localPlayerDidSelectChallengeEvent != null)
				{
					localPlayerDidSelectChallengeEvent(challenge);
				}
			};
			GameCenterManager.localPlayerDidCompleteChallengeEvent += delegate(GameCenterChallenge challenge)
			{
				if (localPlayerDidCompleteChallengeEvent != null)
				{
					localPlayerDidCompleteChallengeEvent(challenge);
				}
			};
			GameCenterManager.remotePlayerDidCompleteChallengeEvent += delegate(GameCenterChallenge challenge)
			{
				if (remotePlayerDidCompleteChallengeEvent != null)
				{
					remotePlayerDidCompleteChallengeEvent(challenge);
				}
			};
			isInitialized = true;
		}
	}

	private static List<FriendScore> lastFriendScores = new List<FriendScore>();

	private static List<GameCenterPlayer> friends = new List<GameCenterPlayer>();

	private static List<GameCenterScore> friendScores = new List<GameCenterScore>();

	private static Action<List<GameCenterScore>> friendScoresLoadedScoresAction = null;

	private static Action<List<GameCenterPlayer>> friendScoresLoadedFriendsAction = null;

	public static List<GameCenterPlayer> Friends
	{
		get
		{
			return friends;
		}
	}

	public static List<FriendScore> LastFriendScores
	{
		get
		{
			return lastFriendScores;
		}
	}

	public static void Initialize()
	{
		EventListener.Initialize();
		lastFriendScores.AddRange(Player.Instance.Friends);
	}

	public static void Login(Action<bool> callback = null)
	{
		if (callback != null)
		{
			EventListener.playerAuthenticated = delegate
			{
				callback(true);
			};
			EventListener.playerFailedToAuthenticate = delegate
			{
				callback(true);
			};
		}
		GameCenterBinding.authenticateLocalPlayer();
	}

	public static void ShowAchievement()
	{
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			GameCenterBinding.showAchievements();
		}
	}

	public static void ShowLeaderboard(GameCenterLeaderboardTimeScope timeScope = GameCenterLeaderboardTimeScope.AllTime)
	{
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			GameCenterBinding.showLeaderboardWithTimeScope(GameCenterLeaderboardTimeScope.AllTime);
		}
	}

	public static void ReportScore(int score, string id)
	{
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			GameCenterBinding.reportScore(score, id);
		}
	}

	public static void ReportAchievementProgress(double progress, string id)
	{
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			GameCenterBinding.reportAchievement(id, (float)progress);
		}
	}

	public static void LoadFriendScores(GameCenterLeaderboardTimeScope timeScope, int start, int end, string leaderboardId, bool loadProfileImages, bool loadLargeProfileImages = true, Action<List<FriendScore>> callback = null)
	{
		Debug.Log("LoadFriendScores");
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			bool isFriendScores = true;
			if (friendScoresLoadedScoresAction != null)
			{
				EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Remove(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
			}
			friendScoresLoadedScoresAction = delegate(List<GameCenterScore> friendScores)
			{
				Debug.Log("friendScoresLoadedScoresAction");
				if (isFriendScores)
				{
					if (friendScores != null)
					{
						GameCenterModel.friendScores.Clear();
						GameCenterModel.friendScores.AddRange(friendScores);
						List<string> list = new List<string>();
						foreach (GameCenterScore friendScore in friendScores)
						{
							list.Add(friendScore.playerId);
						}
						if (friendScoresLoadedFriendsAction != null)
						{
							EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Remove(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
						}
						friendScoresLoadedFriendsAction = delegate(List<GameCenterPlayer> friends)
						{
							FillLastFriendScores(friends, friendScores);
							if (callback != null)
							{
								callback(lastFriendScores);
							}
							if (friendScoresLoadedFriendsAction != null)
							{
								EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Remove(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
								friendScoresLoadedScoresAction = null;
							}
						};
						EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Combine(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
						GameCenterBinding.loadPlayerData(list.ToArray(), loadProfileImages, loadLargeProfileImages);
					}
					if (friendScoresLoadedScoresAction != null)
					{
						EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Remove(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
						friendScoresLoadedScoresAction = null;
					}
				}
			};
			EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Combine(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
			EventListener.retrieveScoresFailed = delegate
			{
				Debug.Log("retrieveScoresFailed");
				if (callback != null)
				{
					callback(LastFriendScores);
				}
			};
			GameCenterBinding.retrieveScores(true, timeScope, start, end, leaderboardId);
			Debug.Log("GameCenterBinding.retrieveScores");
		}
		else if (callback != null)
		{
			callback(LastFriendScores);
		}
	}

	public static void IssueScoreChallenge(long score, long context, string leaderboardId, string[] playerIds, string message)
	{
		Debug.Log("IssueScoreChallenge");
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			GameCenterBinding.issueScoreChallenge(score, context, leaderboardId, playerIds, message);
		}
	}

	private static void FillLastFriendScores(List<GameCenterPlayer> friends, List<GameCenterScore> scores)
	{
		Debug.Log("FillLastFriendScores");
		foreach (GameCenterScore score in scores)
		{
			Debug.Log("id: " + score.playerId + "  sc: " + score.value);
		}
		List<FriendScore> list = new List<FriendScore>();
		if (friends == null || friends.Count <= 0 || scores == null || scores.Count <= 0)
		{
			return;
		}
		GameCenterPlayer friend;
		foreach (GameCenterPlayer friend2 in friends)
		{
			friend = friend2;
			GameCenterScore gameCenterScore = scores.Find((GameCenterScore sc) => friend.playerId.Equals(sc.playerId));
			if (gameCenterScore != null)
			{
				list.Add(new FriendScore(friend, gameCenterScore));
			}
		}
		lastFriendScores.Clear();
		lastFriendScores.AddRange(list);
		Player.Instance.Friends = lastFriendScores;
	}
}
