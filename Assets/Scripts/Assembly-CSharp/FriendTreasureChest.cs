using Event;
using UnityEngine;

public class FriendTreasureChest : TreasureChest
{
	[SerializeField]
	private TUILabel friendAliasLabel;

	[SerializeField]
	private MeshRenderer iconRenderer;

	private GameCenterPlayer friend;

	private bool needLoadPhoto = true;

	public void SetFriend(GameCenterPlayer friend)
	{
		this.friend = friend;
		friendAliasLabel.Text = friend.alias;
		needLoadPhoto = !SetFriendPhoto(friend);
		Debug.Log(string.Empty);
	}

	private void Update()
	{
		if (needLoadPhoto && friend != null)
		{
			needLoadPhoto = !SetFriendPhoto(friend);
		}
	}

	protected override void OnDead()
	{
		EventCenter.Instance.Publish(null, new DestroyFriendChestEvent(friend));
		RefreshTreasure();
		CreateCrash();
		Object.DestroyImmediate(base.gameObject);
	}

	private void OnDestroy()
	{
		if (!needLoadPhoto)
		{
			Object.Destroy(iconRenderer.material.mainTexture);
		}
	}

	private bool SetFriendPhoto(GameCenterPlayer friend)
	{
		if (friend.hasProfilePhoto)
		{
			Debug.Log("friend.hasProfilePhoto");
			iconRenderer.material.mainTexture = friend.profilePhoto;
		}
		return friend.hasProfilePhoto;
	}
}
