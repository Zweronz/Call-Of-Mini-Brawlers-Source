using UnityEngine;

public class MainScene : MonoBehaviour
{
	private string tapPointsLabel = string.Empty;

	private bool autoRefresh;

	private void Start()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJNI.AttachCurrentThread();
		}
		TapjoyPlugin.EnableLogging(true);
		TapjoyPlugin.SetCallbackHandler("MainScene");
		if (Application.platform == RuntimePlatform.Android)
		{
			TapjoyPlugin.RequestTapjoyConnect("bba49f11-b87f-4c0f-9632-21aa810dd6f1", "yiQIURFEeKm0zbOggubu");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			TapjoyPlugin.RequestTapjoyConnect("93e78102-cbd7-4ebf-85cc-315ba83ef2d5", "JWxgS26URM0XotaghqGn");
		}
		TapjoyPlugin.GetDisplayAd();
	}

	public void TapjoyConnectSuccess(string message)
	{
		MonoBehaviour.print(message);
	}

	public void TapjoyConnectFail(string message)
	{
		MonoBehaviour.print(message);
	}

	public void TapPointsLoaded(string message)
	{
		MonoBehaviour.print("TapPointsLoaded: " + message);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void TapPointsLoadedError(string message)
	{
		MonoBehaviour.print("TapPointsLoadedError: " + message);
		tapPointsLabel = "TapPointsLoadedError: " + message;
	}

	public void TapPointsSpent(string message)
	{
		MonoBehaviour.print("TapPointsSpent: " + message);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void TapPointsSpendError(string message)
	{
		MonoBehaviour.print("TapPointsSpendError: " + message);
		tapPointsLabel = "TapPointsSpendError: " + message;
	}

	public void TapPointsAwarded(string message)
	{
		MonoBehaviour.print("TapPointsAwarded: " + message);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void TapPointsAwardError(string message)
	{
		MonoBehaviour.print("TapPointsAwardError: " + message);
		tapPointsLabel = "TapPointsAwardError: " + message;
	}

	public void CurrencyEarned(string message)
	{
		MonoBehaviour.print("CurrencyEarned: " + message);
		tapPointsLabel = "Currency Earned: " + message;
		TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
	}

	public void FullScreenAdLoaded(string message)
	{
		MonoBehaviour.print("FullScreenAdLoaded: " + message);
		tapPointsLabel = "FullScreenAdLoaded: " + message;
		TapjoyPlugin.ShowFullScreenAd();
	}

	public void FullScreenAdError(string message)
	{
		MonoBehaviour.print("FullScreenAdError: " + message);
		tapPointsLabel = "FullScreenAdError: " + message;
	}

	public void DailyRewardAdLoaded(string message)
	{
		MonoBehaviour.print("DailyRewardAd: " + message);
		tapPointsLabel = "DailyRewardAd: " + message;
		TapjoyPlugin.ShowDailyRewardAd();
	}

	public void DailyRewardAdError(string message)
	{
		MonoBehaviour.print("DailyRewardAd: " + message);
		tapPointsLabel = "DailyRewardAd: " + message;
	}

	public void DisplayAdLoaded(string message)
	{
		MonoBehaviour.print("DisplayAdLoaded: " + message);
		tapPointsLabel = "DisplayAdLoaded: " + message;
		TapjoyPlugin.ShowDisplayAd();
	}

	public void DisplayAdError(string message)
	{
		MonoBehaviour.print("DisplayAdError: " + message);
		tapPointsLabel = "DisplayAdError: " + message;
	}

	public void VideoAdStart(string message)
	{
		MonoBehaviour.print("VideoAdStart: " + message);
		tapPointsLabel = "VideoAdStart: " + message;
	}

	public void VideoAdError(string message)
	{
		MonoBehaviour.print("VideoAdError: " + message);
		tapPointsLabel = "VideoAdError: " + message;
	}

	public void VideoAdComplete(string message)
	{
		MonoBehaviour.print("VideoAdComplete: " + message);
		tapPointsLabel = "VideoAdComplete: " + message;
	}

	public void ResetTapPointsLabel()
	{
		tapPointsLabel = "Updating Tap Points...";
	}

	private void OnGUI()
	{
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.alignment = TextAnchor.MiddleCenter;
		gUIStyle.normal.textColor = Color.white;
		float num = Screen.width / 2;
		float num2 = 60f;
		float num3 = 300f;
		float height = 50f;
		float num4 = 20f;
		float num5 = 100f;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		GUI.Label(new Rect(num - 200f, num5, 400f, 25f), "Tapjoy Connect Sample App", gUIStyle);
		num5 += num4 + 10f;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Show Offers"))
		{
			TapjoyPlugin.ShowOffers();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Show Daily Reward Ad"))
		{
			TapjoyPlugin.GetDailyRewardAd();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Get Display Ad"))
		{
			TapjoyPlugin.GetDisplayAd();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Hide Display Ad"))
		{
			TapjoyPlugin.HideDisplayAd();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Toggle Display Ad Auto-Refresh"))
		{
			autoRefresh = !autoRefresh;
			TapjoyPlugin.EnableDisplayAdAutoRefresh(autoRefresh);
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Show FullScreen Ad"))
		{
			TapjoyPlugin.GetFullScreenAd();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Get Tap Points"))
		{
			TapjoyPlugin.GetTapPoints();
			ResetTapPointsLabel();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Spend Tap Points"))
		{
			TapjoyPlugin.SpendTapPoints(10);
			ResetTapPointsLabel();
		}
		num5 += num2;
		if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Award Tap Points"))
		{
			TapjoyPlugin.AwardTapPoints(10);
			ResetTapPointsLabel();
		}
		num5 += num4;
		GUI.Label(new Rect(num - 200f, num5, 400f, 150f), tapPointsLabel, gUIStyle);
	}
}
