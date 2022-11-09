using System;

public class ZS_IapGoldInfo
{
	public string id;

	public string image;

	public float count;

	public float crystal;

	public object data;

	public Func<ZS_IapGoldInfo, int> buyCallBack;
}
