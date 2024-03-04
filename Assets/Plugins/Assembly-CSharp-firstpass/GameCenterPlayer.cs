using System;
using System.Collections.Generic;
using System.IO;
using Prime31;
using UnityEngine;

[Serializable]
public class GameCenterPlayer
{
	public string playerId;

	public string alias;

	public string displayName;

	public bool isFriend;

	private string _profilePhotoPath;

	public bool hasProfilePhoto
	{
		get
		{
			return File.Exists(profilePhotoPath);
		}
	}

	public Texture2D profilePhoto
	{
		get
		{
			if (!hasProfilePhoto)
			{
				return null;
			}
			byte[] data = File.ReadAllBytes(profilePhotoPath);
			Texture2D texture2D = new Texture2D(0, 0);
			if (!texture2D.LoadImage(data))
			{
				return null;
			}
			return texture2D;
		}
	}

	public string profilePhotoPath
	{
		get
		{
			if (_profilePhotoPath == null)
			{
				_profilePhotoPath = Path.Combine(Application.persistentDataPath, string.Format("{0}.png", playerId.Replace(":", string.Empty)));
			}
			return _profilePhotoPath;
		}
	}

	public GameCenterPlayer()
	{
	}

	public GameCenterPlayer(Dictionary<string, object> dict)
	{
		if (dict.ContainsKey("playerId"))
		{
			playerId = dict["playerId"] as string;
		}
		if (dict.ContainsKey("alias"))
		{
			alias = dict["alias"] as string;
		}
		if (dict.ContainsKey("displayName"))
		{
			displayName = dict["displayName"] as string;
		}
		if (dict.ContainsKey("isFriend"))
		{
			isFriend = (bool)dict["isFriend"];
		}
	}

	public static List<GameCenterPlayer> fromJSON(string json)
	{
		List<GameCenterPlayer> list = new List<GameCenterPlayer>();
		List<object> list2 = json.listFromJson();
		foreach (Dictionary<string, object> item in list2)
		{
			list.Add(new GameCenterPlayer(item));
		}
		return list;
	}

	public override string ToString()
	{
		return string.Format("<Player> playerId: {0}, alias: {1}, displayName: {2}, isFriend: {3}", playerId, alias, displayName, isFriend);
	}
}
