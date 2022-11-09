using System;

public class ZS_IapInfo
{
	public string id;

	public string image;

	public float count;

	public int leftPackage;

	public object data;

	public Func<ZS_IapInfo, int> buyCallBack;
}
