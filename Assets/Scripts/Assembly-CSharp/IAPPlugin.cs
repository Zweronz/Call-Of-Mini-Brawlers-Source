using UnityEngine;

public class IAPPlugin
{
	public enum Status
	{
		kUserCancel = -2,
		kError = -1,
		kBuying = 0,
		kSuccess = 1
	}

	public static void NowPurchaseProduct(string productId, string productCount)
	{
		if (MiscPlugin.IsIAPCrack())
		{
			Debug.Log("IsIAPCrack!!!!!!");
		}
	}

	public static int GetPurchaseStatus()
	{
		return 1;
	}

	public static void DoRestoreProduct()
	{
	}

	public static int DoRestoreStatus()
	{
		return 1;
	}

	public static string[] DoRestoreGetProductId()
	{
		string empty = string.Empty;
		return empty.Split('|');
	}
}
