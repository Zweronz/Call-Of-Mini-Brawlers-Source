using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PathExists
{
	public static bool Exists(string game, string company, string packageName)
	{
		bool exists = false;

		#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\" + company + "\\" + game);

			if (key != null)
			{
				key.Close();
				exists = true;
			}

		#elif UNITY_ANDROID
			exists = Directory.Exists(Directory.GetParent(Directory.GetParent(Application.persistentDataPath).FullName).FullName + "/" + packageName);
		#endif

		return exists;
	}
}
