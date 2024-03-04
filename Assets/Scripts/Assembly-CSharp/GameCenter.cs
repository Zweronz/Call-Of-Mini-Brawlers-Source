using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameCenter
{
	public class FriendScore
	{
		public IUserProfile friend;

		public IScore score;
	}

	private static bool isReloadedFriends = false;

	private static bool isLoadingFriends = false;

	private static bool isLoadedFriends = false;

	private static SynchronizeLock synchronizeLock;

	private static Action loadedFriendsCallback = null;

	private static List<IUserProfile> friends = new List<IUserProfile>();

	private static Dictionary<string, List<IScore>> leaderboardScores = new Dictionary<string, List<IScore>>();

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
		if (!Social.localUser.authenticated)
		{
			return;
		}
		isLoadingFriends = true;
		Social.localUser.LoadFriends((Action<bool>)Delegate.Combine((Action<bool>)delegate(bool success)
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
		}, callback));
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
		synchronizeLock = new SynchronizeLock();
		synchronizeLock.SetCallback(delegate
		{
			if (callback != null)
			{
				callback(IntegrateFriendAndScore(Friends, (!leaderboardScores.ContainsKey(leaderboardId)) ? null : leaderboardScores[leaderboardId]));
			}
		});
		if (reloadFriends)
		{
			synchronizeLock.Lock(1u);
			LoadFriends(delegate
			{
				synchronizeLock.Unlock(1u);
			});
		}
		else if (isLoadingFriends)
		{
			synchronizeLock.Lock(1u);
			loadedFriendsCallback = delegate
			{
				synchronizeLock.Unlock(1u);
			};
		}
		if (reloadFriends || (!isLoadingFriends && !isLoadedFriends))
		{
			synchronizeLock.Lock(1u);
			LoadFriends(delegate
			{
				synchronizeLock.Unlock(1u);
			});
		}
		synchronizeLock.Lock(1u);
		LoadScores(leaderboardId, delegate(IScore[] scoreList)
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
		});
	}

	private static List<FriendScore> IntegrateFriendAndScore(List<IUserProfile> friends, List<IScore> scores)
	{
		List<FriendScore> result = new List<FriendScore>();
		if (friends != null && friends.Count > 0 && scores != null && scores.Count > 0)
		{
			IUserProfile friend;
			foreach (IUserProfile friend2 in friends)
			{
				friend = friend2;
				IScore score = scores.Find((IScore sc) => friend.id.Equals(sc.userID));
			}
		}
		return result;
	}
}
