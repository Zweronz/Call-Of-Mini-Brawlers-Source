using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameCenter
{
	public class FriendScore
	{
		public IUserProfile friend;

		public IScore score;
	}

	[CompilerGenerated]
	private sealed class _003CLoadFriendScores_003Ec__AnonStorey23
	{
		internal Action<List<FriendScore>> callback;

		internal string leaderboardId;

		internal void _003C_003Em__1A()
		{
			if (callback != null)
			{
				callback(IntegrateFriendAndScore(Friends, (!leaderboardScores.ContainsKey(leaderboardId)) ? null : leaderboardScores[leaderboardId]));
			}
		}

		internal void _003C_003Em__1E(IScore[] scoreList)
		{
			if (scoreList != null)
			{
				if (!leaderboardScores.ContainsKey(leaderboardId))
				{
					leaderboardScores.Add(leaderboardId, new List<IScore>());
				}
				leaderboardScores[leaderboardId].Clear();
				leaderboardScores[leaderboardId].AddRange(scoreList);
			}
			synchronizeLock.Unlock(1u);
		}
	}

	[CompilerGenerated]
	private sealed class _003CIntegrateFriendAndScore_003Ec__AnonStorey24
	{
		internal IUserProfile friend;

		internal bool _003C_003Em__1F(IScore sc)
		{
			return friend.id.Equals(sc.userID);
		}
	}

	private static bool isReloadedFriends = false;

	private static bool isLoadingFriends = false;

	private static bool isLoadedFriends = false;

	private static SynchronizeLock synchronizeLock;

	private static Action loadedFriendsCallback = null;

	private static List<IUserProfile> friends = new List<IUserProfile>();

	private static Dictionary<string, List<IScore>> leaderboardScores = new Dictionary<string, List<IScore>>();

	[CompilerGenerated]
	private static Action<bool> _003C_003Ef__am_0024cache7;

	[CompilerGenerated]
	private static Action<bool> _003C_003Ef__am_0024cache8;

	[CompilerGenerated]
	private static Action _003C_003Ef__am_0024cache9;

	[CompilerGenerated]
	private static Action<bool> _003C_003Ef__am_0024cacheA;

	public static List<IUserProfile> Friends
	{
		get
		{
			if (isReloadedFriends)
			{
				if (Social.localUser.friends != null)
				{
					friends.Clear();
					friends.AddRange(Social.localUser.friends);
				}
				isReloadedFriends = false;
			}
			return friends;
		}
	}

	public static bool IsLoadedFriends
	{
		get
		{
			return isLoadedFriends;
		}
	}

	public static void Login(Action<bool> callback = null)
	{
		Social.localUser.Authenticate(callback);
	}

	public static void ShowAchievement()
	{
		if (Social.localUser.authenticated)
		{
			Social.ShowAchievementsUI();
		}
	}

	public static void ShowLeaderboard()
	{
		if (Social.localUser.authenticated)
		{
			Social.ShowLeaderboardUI();
		}
	}

	public static void ReportScore(long score, string id, Action<bool> callback = null)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportScore(score, id, callback);
		}
		else if (callback != null)
		{
			callback(false);
		}
	}

	public static void ReportAchievementProgress(double progress, string id, Action<bool> callback = null)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportProgress(id, progress, callback);
		}
		else if (callback != null)
		{
			callback(false);
		}
	}

	public static void LoadFriends(Action<bool> callback = null)
	{
		if (Social.localUser.authenticated)
		{
			isLoadingFriends = true;
			ILocalUser localUser = Social.localUser;
			if (_003C_003Ef__am_0024cache7 == null)
			{
				_003C_003Ef__am_0024cache7 = _003CLoadFriends_003Em__19;
			}
			localUser.LoadFriends((Action<bool>)Delegate.Combine(_003C_003Ef__am_0024cache7, callback));
		}
	}

	public static void LoadScores(string leaderboardId, Action<IScore[]> callback)
	{
		if (Social.localUser.authenticated)
		{
			Social.LoadScores(leaderboardId, callback);
		}
		else if (callback != null)
		{
			callback(null);
		}
	}

	public static void LoadFriendScores(string leaderboardId, Action<List<FriendScore>> callback, bool reloadFriends = false)
	{
		_003CLoadFriendScores_003Ec__AnonStorey23 _003CLoadFriendScores_003Ec__AnonStorey = new _003CLoadFriendScores_003Ec__AnonStorey23();
		_003CLoadFriendScores_003Ec__AnonStorey.callback = callback;
		_003CLoadFriendScores_003Ec__AnonStorey.leaderboardId = leaderboardId;
		synchronizeLock = new SynchronizeLock();
		synchronizeLock.SetCallback(_003CLoadFriendScores_003Ec__AnonStorey._003C_003Em__1A);
		if (reloadFriends)
		{
			synchronizeLock.Lock(1u);
			if (_003C_003Ef__am_0024cache8 == null)
			{
				_003C_003Ef__am_0024cache8 = _003CLoadFriendScores_003Em__1B;
			}
			LoadFriends(_003C_003Ef__am_0024cache8);
		}
		else if (isLoadingFriends)
		{
			synchronizeLock.Lock(1u);
			if (_003C_003Ef__am_0024cache9 == null)
			{
				_003C_003Ef__am_0024cache9 = _003CLoadFriendScores_003Em__1C;
			}
			loadedFriendsCallback = _003C_003Ef__am_0024cache9;
		}
		if (reloadFriends || (!isLoadingFriends && !isLoadedFriends))
		{
			synchronizeLock.Lock(1u);
			if (_003C_003Ef__am_0024cacheA == null)
			{
				_003C_003Ef__am_0024cacheA = _003CLoadFriendScores_003Em__1D;
			}
			LoadFriends(_003C_003Ef__am_0024cacheA);
		}
		synchronizeLock.Lock(1u);
		LoadScores(_003CLoadFriendScores_003Ec__AnonStorey.leaderboardId, _003CLoadFriendScores_003Ec__AnonStorey._003C_003Em__1E);
	}

	private static List<FriendScore> IntegrateFriendAndScore(List<IUserProfile> friends, List<IScore> scores)
	{
		List<FriendScore> result = new List<FriendScore>();
		if (friends != null && friends.Count > 0 && scores != null && scores.Count > 0)
		{
			_003CIntegrateFriendAndScore_003Ec__AnonStorey24 _003CIntegrateFriendAndScore_003Ec__AnonStorey = new _003CIntegrateFriendAndScore_003Ec__AnonStorey24();
			{
				foreach (IUserProfile friend in friends)
				{
					_003CIntegrateFriendAndScore_003Ec__AnonStorey.friend = friend;
					IScore score = scores.Find(_003CIntegrateFriendAndScore_003Ec__AnonStorey._003C_003Em__1F);
				}
				return result;
			}
		}
		return result;
	}

	[CompilerGenerated]
	private static void _003CLoadFriends_003Em__19(bool success)
	{
		if (success)
		{
			isLoadedFriends = true;
			isReloadedFriends = true;
		}
		isLoadingFriends = false;
		if (loadedFriendsCallback != null)
		{
			loadedFriendsCallback();
			loadedFriendsCallback = null;
		}
	}

	[CompilerGenerated]
	private static void _003CLoadFriendScores_003Em__1B(bool success)
	{
		synchronizeLock.Unlock(1u);
	}

	[CompilerGenerated]
	private static void _003CLoadFriendScores_003Em__1C()
	{
		synchronizeLock.Unlock(1u);
	}

	[CompilerGenerated]
	private static void _003CLoadFriendScores_003Em__1D(bool success)
	{
		synchronizeLock.Unlock(1u);
	}
}
