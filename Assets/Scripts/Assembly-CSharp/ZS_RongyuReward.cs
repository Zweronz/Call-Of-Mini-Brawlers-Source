using System;
using UnityEngine;

public class ZS_RongyuReward : MonoBehaviour
{
	public ZS_RongyuInfo bindInfo;

	private Func<ZS_RongyuInfo, ZS_RongyuInfo> clickEventProcess;

	public void SetEventHandle(Func<ZS_RongyuInfo, ZS_RongyuInfo> handle)
	{
		clickEventProcess = handle;
	}

	public void ClearEventHandle()
	{
		clickEventProcess = null;
	}

	public ZS_RongyuInfo NotifyClick()
	{
		if (clickEventProcess != null && bindInfo != null)
		{
			return clickEventProcess(bindInfo);
		}
		return null;
	}
}
