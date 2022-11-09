using System;

public class ZS_RongyuInfo
{
	public string id;

	public string icon;

	public string message;

	public int rewardCount;

	public float compPercent;

	public ZS_Money money;

	public bool isCompleted;

	public object data;

	public Func<ZS_RongyuInfo, ZS_RongyuInfo> callBack;
}
