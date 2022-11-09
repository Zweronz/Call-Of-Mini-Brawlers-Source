using UnityEngine;

public class FriendPrefab : MonoBehaviour
{
	public TUIMeshSprite icon;

	public TUILabel idLab;

	public TUILabel nameLab;

	public TUILabel scoreLab;

	private GameCenterModel.FriendScore fs;

	private bool needLoadPhoto = true;

	public void Set(Texture2D image, string id, string name, float score)
	{
		if (null != image)
		{
			icon.UseCustomize = true;
			icon.CustomizeTexture = image;
			icon.CustomizeRect = new Rect(0f, 0f, image.width, image.height);
		}
		idLab.Text = id;
		nameLab.Text = name;
		scoreLab.Text = score.ToString();
	}

	public void Set(GameCenterModel.FriendScore fs)
	{
		this.fs = fs;
		idLab.Text = fs.friend.playerId;
		nameLab.Text = fs.friend.displayName;
		scoreLab.Text = fs.score.value.ToString();
	}

	private bool SetFriendPhoto(GameCenterPlayer friend)
	{
		if (friend.hasProfilePhoto)
		{
			Texture2D profilePhoto = friend.profilePhoto;
			if (null != profilePhoto)
			{
				float num = profilePhoto.width / profilePhoto.height;
				if (profilePhoto.width > 76)
				{
					profilePhoto.Resize(76, Mathf.RoundToInt(76f / num));
				}
				else if (profilePhoto.height > 76)
				{
					profilePhoto.Resize(Mathf.RoundToInt(76f * num), 76);
				}
				icon.UseCustomize = true;
				icon.CustomizeTexture = profilePhoto;
				icon.CustomizeRect = new Rect(0f, 0f, profilePhoto.width, profilePhoto.height);
			}
		}
		return friend.hasProfilePhoto;
	}

	private void Update()
	{
		if (needLoadPhoto && fs != null)
		{
			needLoadPhoto = !SetFriendPhoto(fs.friend);
		}
	}
}
