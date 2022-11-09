using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TestGameCenterFriend : MonoBehaviour
{
	public string leaderboardid;

	public TUIScrollList friendList;

	public TUIScrollList friendScoreList;

	public GameObject friendPrefab;

	private void Start()
	{
		GameCenterModel.LoadFriendScores(GameCenterLeaderboardTimeScope.AllTime, 1, 50, string.Empty, true, false, _003CStart_003Em__5A);
	}

	private void ShowFriends(List<IUserProfile> friends)
	{
		if (friends == null)
		{
			return;
		}
		foreach (IUserProfile friend in friends)
		{
			friendList.Add(CreatePrefab(friend.image, friend.id, friend.userName));
		}
	}

	private void Show(List<GameCenterModel.FriendScore> fslist)
	{
		if (fslist == null)
		{
			return;
		}
		foreach (GameCenterModel.FriendScore item in fslist)
		{
			friendList.Add(CreatePrefab(item));
		}
	}

	private void ShowFriends(List<GameCenterPlayer> friends)
	{
		if (friends == null)
		{
			return;
		}
		foreach (GameCenterPlayer friend in friends)
		{
			friendList.Add(CreatePrefab(friend.profilePhoto, friend.playerId, friend.alias));
		}
	}

	private void ShowFriendScores(List<GameCenter.FriendScore> fsList)
	{
		if (fsList == null)
		{
			return;
		}
		foreach (GameCenter.FriendScore fs in fsList)
		{
			friendScoreList.Add(CreatePrefab(fs.friend.image, fs.friend.id, fs.friend.userName, fs.score.value));
		}
	}

	private TUIControl CreatePrefab(GameCenterModel.FriendScore fs)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(friendPrefab);
		FriendPrefab component = gameObject.GetComponent<FriendPrefab>();
		return gameObject.GetComponent<TUIControl>();
	}

	private TUIControl CreatePrefab(Texture2D image, string id, string name, float score = -1f)
	{
		if (null != image)
		{
			Debug.Log("width: " + image.width + "   hegiht: " + image.height);
		}
		GameObject gameObject = (GameObject)Object.Instantiate(friendPrefab);
		FriendPrefab component = gameObject.GetComponent<FriendPrefab>();
		component.Set(image, id, name, score);
		return gameObject.GetComponent<TUIControl>();
	}

	[CompilerGenerated]
	private void _003CStart_003Em__5A(List<GameCenterModel.FriendScore> friendScores)
	{
		Show(friendScores);
	}
}
