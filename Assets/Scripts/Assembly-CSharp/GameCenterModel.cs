using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache1B;

		[CompilerGenerated]
		private static Action<List<GameCenterPlayer>> _003C_003Ef__am_0024cache1C;

		[CompilerGenerated]
		private static Action _003C_003Ef__am_0024cache1D;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache1E;

		[CompilerGenerated]
		private static Action _003C_003Ef__am_0024cache1F;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache20;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache21;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache22;

		[CompilerGenerated]
		private static Action<List<GameCenterLeaderboard>> _003C_003Ef__am_0024cache23;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache24;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache25;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache26;

		[CompilerGenerated]
		private static Action<List<GameCenterScore>> _003C_003Ef__am_0024cache27;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache28;

		[CompilerGenerated]
		private static Action<List<GameCenterScore>> _003C_003Ef__am_0024cache29;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache2A;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache2B;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache2C;

		[CompilerGenerated]
		private static Action<List<GameCenterAchievement>> _003C_003Ef__am_0024cache2D;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache2E;

		[CompilerGenerated]
		private static Action _003C_003Ef__am_0024cache2F;

		[CompilerGenerated]
		private static Action<string> _003C_003Ef__am_0024cache30;

		[CompilerGenerated]
		private static Action<List<GameCenterAchievementMetadata>> _003C_003Ef__am_0024cache31;

		[CompilerGenerated]
		private static Action<GameCenterChallenge> _003C_003Ef__am_0024cache32;

		[CompilerGenerated]
		private static Action<GameCenterChallenge> _003C_003Ef__am_0024cache33;

		[CompilerGenerated]
		private static Action<GameCenterChallenge> _003C_003Ef__am_0024cache34;

		public static void Initialize()
		{
			if (!isInitialized)
			{
				if (_003C_003Ef__am_0024cache1B == null)
				{
					_003C_003Ef__am_0024cache1B = _003CInitialize_003Em__26;
				}
				GameCenterManager.loadPlayerDataFailed += _003C_003Ef__am_0024cache1B;
				if (_003C_003Ef__am_0024cache1C == null)
				{
					_003C_003Ef__am_0024cache1C = _003CInitialize_003Em__27;
				}
				GameCenterManager.playerDataLoaded += _003C_003Ef__am_0024cache1C;
				if (_003C_003Ef__am_0024cache1D == null)
				{
					_003C_003Ef__am_0024cache1D = _003CInitialize_003Em__28;
				}
				GameCenterManager.playerAuthenticated += _003C_003Ef__am_0024cache1D;
				if (_003C_003Ef__am_0024cache1E == null)
				{
					_003C_003Ef__am_0024cache1E = _003CInitialize_003Em__29;
				}
				GameCenterManager.playerFailedToAuthenticate += _003C_003Ef__am_0024cache1E;
				if (_003C_003Ef__am_0024cache1F == null)
				{
					_003C_003Ef__am_0024cache1F = _003CInitialize_003Em__2A;
				}
				GameCenterManager.playerLoggedOut += _003C_003Ef__am_0024cache1F;
				if (_003C_003Ef__am_0024cache20 == null)
				{
					_003C_003Ef__am_0024cache20 = _003CInitialize_003Em__2B;
				}
				GameCenterManager.profilePhotoLoaded += _003C_003Ef__am_0024cache20;
				if (_003C_003Ef__am_0024cache21 == null)
				{
					_003C_003Ef__am_0024cache21 = _003CInitialize_003Em__2C;
				}
				GameCenterManager.profilePhotoFailed += _003C_003Ef__am_0024cache21;
				if (_003C_003Ef__am_0024cache22 == null)
				{
					_003C_003Ef__am_0024cache22 = _003CInitialize_003Em__2D;
				}
				GameCenterManager.loadCategoryTitlesFailed += _003C_003Ef__am_0024cache22;
				if (_003C_003Ef__am_0024cache23 == null)
				{
					_003C_003Ef__am_0024cache23 = _003CInitialize_003Em__2E;
				}
				GameCenterManager.categoriesLoaded += _003C_003Ef__am_0024cache23;
				if (_003C_003Ef__am_0024cache24 == null)
				{
					_003C_003Ef__am_0024cache24 = _003CInitialize_003Em__2F;
				}
				GameCenterManager.reportScoreFailed += _003C_003Ef__am_0024cache24;
				if (_003C_003Ef__am_0024cache25 == null)
				{
					_003C_003Ef__am_0024cache25 = _003CInitialize_003Em__30;
				}
				GameCenterManager.reportScoreFinished += _003C_003Ef__am_0024cache25;
				if (_003C_003Ef__am_0024cache26 == null)
				{
					_003C_003Ef__am_0024cache26 = _003CInitialize_003Em__31;
				}
				GameCenterManager.retrieveScoresFailed += _003C_003Ef__am_0024cache26;
				if (_003C_003Ef__am_0024cache27 == null)
				{
					_003C_003Ef__am_0024cache27 = _003CInitialize_003Em__32;
				}
				GameCenterManager.scoresLoaded += _003C_003Ef__am_0024cache27;
				if (_003C_003Ef__am_0024cache28 == null)
				{
					_003C_003Ef__am_0024cache28 = _003CInitialize_003Em__33;
				}
				GameCenterManager.retrieveScoresForPlayerIdFailed += _003C_003Ef__am_0024cache28;
				if (_003C_003Ef__am_0024cache29 == null)
				{
					_003C_003Ef__am_0024cache29 = _003CInitialize_003Em__34;
				}
				GameCenterManager.scoresForPlayerIdLoaded += _003C_003Ef__am_0024cache29;
				if (_003C_003Ef__am_0024cache2A == null)
				{
					_003C_003Ef__am_0024cache2A = _003CInitialize_003Em__35;
				}
				GameCenterManager.reportAchievementFailed += _003C_003Ef__am_0024cache2A;
				if (_003C_003Ef__am_0024cache2B == null)
				{
					_003C_003Ef__am_0024cache2B = _003CInitialize_003Em__36;
				}
				GameCenterManager.reportAchievementFinished += _003C_003Ef__am_0024cache2B;
				if (_003C_003Ef__am_0024cache2C == null)
				{
					_003C_003Ef__am_0024cache2C = _003CInitialize_003Em__37;
				}
				GameCenterManager.loadAchievementsFailed += _003C_003Ef__am_0024cache2C;
				if (_003C_003Ef__am_0024cache2D == null)
				{
					_003C_003Ef__am_0024cache2D = _003CInitialize_003Em__38;
				}
				GameCenterManager.achievementsLoaded += _003C_003Ef__am_0024cache2D;
				if (_003C_003Ef__am_0024cache2E == null)
				{
					_003C_003Ef__am_0024cache2E = _003CInitialize_003Em__39;
				}
				GameCenterManager.resetAchievementsFailed += _003C_003Ef__am_0024cache2E;
				if (_003C_003Ef__am_0024cache2F == null)
				{
					_003C_003Ef__am_0024cache2F = _003CInitialize_003Em__3A;
				}
				GameCenterManager.resetAchievementsFinished += _003C_003Ef__am_0024cache2F;
				if (_003C_003Ef__am_0024cache30 == null)
				{
					_003C_003Ef__am_0024cache30 = _003CInitialize_003Em__3B;
				}
				GameCenterManager.retrieveAchievementMetadataFailed += _003C_003Ef__am_0024cache30;
				if (_003C_003Ef__am_0024cache31 == null)
				{
					_003C_003Ef__am_0024cache31 = _003CInitialize_003Em__3C;
				}
				GameCenterManager.achievementMetadataLoaded += _003C_003Ef__am_0024cache31;
				if (_003C_003Ef__am_0024cache32 == null)
				{
					_003C_003Ef__am_0024cache32 = _003CInitialize_003Em__3D;
				}
				GameCenterManager.localPlayerDidSelectChallengeEvent += _003C_003Ef__am_0024cache32;
				if (_003C_003Ef__am_0024cache33 == null)
				{
					_003C_003Ef__am_0024cache33 = _003CInitialize_003Em__3E;
				}
				GameCenterManager.localPlayerDidCompleteChallengeEvent += _003C_003Ef__am_0024cache33;
				if (_003C_003Ef__am_0024cache34 == null)
				{
					_003C_003Ef__am_0024cache34 = _003CInitialize_003Em__3F;
				}
				GameCenterManager.remotePlayerDidCompleteChallengeEvent += _003C_003Ef__am_0024cache34;
				isInitialized = true;
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__26(string message)
		{
			if (loadPlayerDataFailed != null)
			{
				loadPlayerDataFailed(message);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__27(List<GameCenterPlayer> playerData)
		{
			if (playerDataLoaded != null)
			{
				playerDataLoaded(playerData);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__28()
		{
			if (playerAuthenticated != null)
			{
				playerAuthenticated();
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__29(string error)
		{
			if (playerFailedToAuthenticate != null)
			{
				playerFailedToAuthenticate(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2A()
		{
			if (playerLoggedOut != null)
			{
				playerLoggedOut();
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2B(string path)
		{
			if (profilePhotoLoaded != null)
			{
				profilePhotoLoaded(path);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2C(string error)
		{
			if (profilePhotoFailed != null)
			{
				profilePhotoFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2D(string error)
		{
			if (loadCategoryTitlesFailed != null)
			{
				loadCategoryTitlesFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2E(List<GameCenterLeaderboard> leaderboardList)
		{
			if (categoriesLoaded != null)
			{
				categoriesLoaded(leaderboardList);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__2F(string error)
		{
			if (reportScoreFailed != null)
			{
				reportScoreFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__30(string category)
		{
			if (reportScoreFinished != null)
			{
				reportScoreFinished(category);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__31(string error)
		{
			if (retrieveScoresFailed != null)
			{
				retrieveScoresFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__32(List<GameCenterScore> scoreList)
		{
			if (scoresLoaded != null)
			{
				scoresLoaded(scoreList);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__33(string error)
		{
			if (retrieveScoresForPlayerIdFailed != null)
			{
				retrieveScoresForPlayerIdFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__34(List<GameCenterScore> scoreList)
		{
			if (scoresForPlayerIdLoaded != null)
			{
				scoresForPlayerIdLoaded(scoreList);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__35(string error)
		{
			if (reportAchievementFailed != null)
			{
				reportAchievementFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__36(string identifier)
		{
			if (reportAchievementFinished != null)
			{
				reportAchievementFinished(identifier);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__37(string error)
		{
			if (loadAchievementsFailed != null)
			{
				loadAchievementsFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__38(List<GameCenterAchievement> achievementList)
		{
			if (achievementsLoaded != null)
			{
				achievementsLoaded(achievementList);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__39(string error)
		{
			if (resetAchievementsFailed != null)
			{
				resetAchievementsFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3A()
		{
			if (resetAchievementsFinished != null)
			{
				resetAchievementsFinished();
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3B(string error)
		{
			if (retrieveAchievementMetadataFailed != null)
			{
				retrieveAchievementMetadataFailed(error);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3C(List<GameCenterAchievementMetadata> achievementMetadataList)
		{
			if (achievementMetadataLoaded != null)
			{
				achievementMetadataLoaded(achievementMetadataList);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3D(GameCenterChallenge challenge)
		{
			if (localPlayerDidSelectChallengeEvent != null)
			{
				localPlayerDidSelectChallengeEvent(challenge);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3E(GameCenterChallenge challenge)
		{
			if (localPlayerDidCompleteChallengeEvent != null)
			{
				localPlayerDidCompleteChallengeEvent(challenge);
			}
		}

		[CompilerGenerated]
		private static void _003CInitialize_003Em__3F(GameCenterChallenge challenge)
		{
			if (remotePlayerDidCompleteChallengeEvent != null)
			{
				remotePlayerDidCompleteChallengeEvent(challenge);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CLogin_003Ec__AnonStorey25
	{
		internal Action<bool> callback;

		internal void _003C_003Em__20()
		{
			callback(true);
		}

		internal void _003C_003Em__21(string error)
		{
			callback(true);
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoadFriendScores_003Ec__AnonStorey28
	{
		internal Action<List<FriendScore>> callback;

		internal bool loadProfileImages;

		internal bool loadLargeProfileImages;
	}

	[CompilerGenerated]
	private sealed class _003CLoadFriendScores_003Ec__AnonStorey26
	{
		private sealed class _003CLoadFriendScores_003Ec__AnonStorey27
		{
			internal List<GameCenterScore> friendScores;

			internal _003CLoadFriendScores_003Ec__AnonStorey28 _003C_003Ef__ref_002440;

			internal _003CLoadFriendScores_003Ec__AnonStorey26 _003C_003Ef__ref_002438;

			internal void _003C_003Em__25(List<GameCenterPlayer> friends)
			{
				FillLastFriendScores(friends, friendScores);
				if (_003C_003Ef__ref_002440.callback != null)
				{
					_003C_003Ef__ref_002440.callback(lastFriendScores);
				}
				if (friendScoresLoadedFriendsAction != null)
				{
					EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Remove(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
					friendScoresLoadedScoresAction = null;
				}
			}
		}

		internal bool isFriendScores;

		internal _003CLoadFriendScores_003Ec__AnonStorey28 _003C_003Ef__ref_002440;

		internal void _003C_003Em__22(List<GameCenterScore> friendScores)
		{
			_003CLoadFriendScores_003Ec__AnonStorey27 _003CLoadFriendScores_003Ec__AnonStorey = new _003CLoadFriendScores_003Ec__AnonStorey27();
			_003CLoadFriendScores_003Ec__AnonStorey._003C_003Ef__ref_002440 = _003C_003Ef__ref_002440;
			_003CLoadFriendScores_003Ec__AnonStorey._003C_003Ef__ref_002438 = this;
			_003CLoadFriendScores_003Ec__AnonStorey.friendScores = friendScores;
			Debug.Log("friendScoresLoadedScoresAction");
			if (!isFriendScores)
			{
				return;
			}
			if (_003CLoadFriendScores_003Ec__AnonStorey.friendScores != null)
			{
				GameCenterModel.friendScores.Clear();
				GameCenterModel.friendScores.AddRange(_003CLoadFriendScores_003Ec__AnonStorey.friendScores);
				List<string> list = new List<string>();
				foreach (GameCenterScore friendScore in _003CLoadFriendScores_003Ec__AnonStorey.friendScores)
				{
					list.Add(friendScore.playerId);
				}
				if (friendScoresLoadedFriendsAction != null)
				{
					EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Remove(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
				}
				friendScoresLoadedFriendsAction = _003CLoadFriendScores_003Ec__AnonStorey._003C_003Em__25;
				EventListener.playerDataLoaded = (Action<List<GameCenterPlayer>>)Delegate.Combine(EventListener.playerDataLoaded, friendScoresLoadedFriendsAction);
				GameCenterBinding.loadPlayerData(list.ToArray(), _003C_003Ef__ref_002440.loadProfileImages, _003C_003Ef__ref_002440.loadLargeProfileImages);
			}
			if (friendScoresLoadedScoresAction != null)
			{
				EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Remove(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
				friendScoresLoadedScoresAction = null;
			}
		}

		internal void _003C_003Em__23(string error)
		{
			Debug.Log("retrieveScoresFailed");
			if (_003C_003Ef__ref_002440.callback != null)
			{
				_003C_003Ef__ref_002440.callback(LastFriendScores);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CFillLastFriendScores_003Ec__AnonStorey29
	{
		internal GameCenterPlayer friend;

		internal bool _003C_003Em__24(GameCenterScore sc)
		{
			return friend.playerId.Equals(sc.playerId);
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
		_003CLogin_003Ec__AnonStorey25 _003CLogin_003Ec__AnonStorey = new _003CLogin_003Ec__AnonStorey25();
		_003CLogin_003Ec__AnonStorey.callback = callback;
		if (_003CLogin_003Ec__AnonStorey.callback != null)
		{
			EventListener.playerAuthenticated = _003CLogin_003Ec__AnonStorey._003C_003Em__20;
			EventListener.playerFailedToAuthenticate = _003CLogin_003Ec__AnonStorey._003C_003Em__21;
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
		_003CLoadFriendScores_003Ec__AnonStorey28 _003CLoadFriendScores_003Ec__AnonStorey = new _003CLoadFriendScores_003Ec__AnonStorey28();
		_003CLoadFriendScores_003Ec__AnonStorey.callback = callback;
		_003CLoadFriendScores_003Ec__AnonStorey.loadProfileImages = loadProfileImages;
		_003CLoadFriendScores_003Ec__AnonStorey.loadLargeProfileImages = loadLargeProfileImages;
		Debug.Log("LoadFriendScores");
		if (GameCenterBinding.isPlayerAuthenticated())
		{
			_003CLoadFriendScores_003Ec__AnonStorey26 _003CLoadFriendScores_003Ec__AnonStorey2 = new _003CLoadFriendScores_003Ec__AnonStorey26();
			_003CLoadFriendScores_003Ec__AnonStorey2._003C_003Ef__ref_002440 = _003CLoadFriendScores_003Ec__AnonStorey;
			_003CLoadFriendScores_003Ec__AnonStorey2.isFriendScores = true;
			if (friendScoresLoadedScoresAction != null)
			{
				EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Remove(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
			}
			friendScoresLoadedScoresAction = _003CLoadFriendScores_003Ec__AnonStorey2._003C_003Em__22;
			EventListener.scoresLoaded = (Action<List<GameCenterScore>>)Delegate.Combine(EventListener.scoresLoaded, friendScoresLoadedScoresAction);
			EventListener.retrieveScoresFailed = _003CLoadFriendScores_003Ec__AnonStorey2._003C_003Em__23;
			GameCenterBinding.retrieveScores(true, timeScope, start, end, leaderboardId);
			Debug.Log("GameCenterBinding.retrieveScores");
		}
		else if (_003CLoadFriendScores_003Ec__AnonStorey.callback != null)
		{
			_003CLoadFriendScores_003Ec__AnonStorey.callback(LastFriendScores);
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
		_003CFillLastFriendScores_003Ec__AnonStorey29 _003CFillLastFriendScores_003Ec__AnonStorey = new _003CFillLastFriendScores_003Ec__AnonStorey29();
		foreach (GameCenterPlayer friend in friends)
		{
			_003CFillLastFriendScores_003Ec__AnonStorey.friend = friend;
			GameCenterScore gameCenterScore = scores.Find(_003CFillLastFriendScores_003Ec__AnonStorey._003C_003Em__24);
			if (gameCenterScore != null)
			{
				list.Add(new FriendScore(_003CFillLastFriendScores_003Ec__AnonStorey.friend, gameCenterScore));
			}
		}
		lastFriendScores.Clear();
		lastFriendScores.AddRange(list);
		Player.Instance.Friends = lastFriendScores;
	}
}
