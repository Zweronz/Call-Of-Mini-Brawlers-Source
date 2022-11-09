using System;
using System.Collections.Generic;

public class MiscPlugin
{
	public static long GetSystemSecond()
	{
		TimeSpan timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
		return (long)timeSpan.TotalSeconds;
	}

	public static int OnCheckPhotoSaveStatus()
	{
		return 1;
	}

	public static void OnResetPhotoSaveStatus()
	{
	}

	public static void OpenLocalCameraRoll()
	{
	}

	public static void ShowIndicatorSystem(int style, bool iPad, float r, float g, float b, float a)
	{
	}

	public static void ShowIndicatorSystem_int(int style, int iPad, float r, float g, float b, float a)
	{
	}

	public static void HideIndicatorSystem()
	{
	}

	public static int GetIOSYear()
	{
		return DateTime.Now.Year;
	}

	public static int GetIOSMonth()
	{
		return DateTime.Now.Month;
	}

	public static int GetIOSDay()
	{
		return DateTime.Now.Day;
	}

	public static int GetIOSHour()
	{
		return DateTime.Now.Hour;
	}

	public static int GetIOSMin()
	{
		return DateTime.Now.Minute;
	}

	public static int GetIOSSec()
	{
		return DateTime.Now.Second;
	}

	public static bool IsJailbreak()
	{
		return true;
	}

	public static bool IsIAPCrack()
	{
		return false;
	}

	public static int ShowMessageBox(string title, string message, List<string> buttons)
	{
		return 0;
	}

	public static string OpenMessageBoxAsynchronously(string title, string message, List<string> buttons = null)
	{
		return string.Empty;
	}

	public static int QueryAsynchronousMessageBoxBtnClick(string messageBoxUUID)
	{
		return 0;
	}

	public static void ReleaseAsynchronousMessageBox(string messageBoxUUID)
	{
	}

	public static bool isIOS6OrHigherRequired()
	{
		return false;
	}
}
