using System;

[Serializable]
public class Crystal2Gold
{
	public string id;

	public float crystal;

	public float gold;

	public float UpgradeByHeroLevel(int heroLevel)
	{
		return (heroLevel != 1) ? ((float)(int)(gold * (float)heroLevel * 0.5f)) : gold;
	}
}
