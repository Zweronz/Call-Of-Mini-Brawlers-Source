using System;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStreetCommon
{
	private static System.Random random;

	private static string documentsPath;

	public static readonly int LowPerformance = 50;

	public static string SavePath
	{
		get
		{
			return DocumentsPath + "/ZombieStreet";
		}
	}

	public static string DocumentsPath
	{
		get
		{
			if (documentsPath == null)
			{
				documentsPath = Application.dataPath;
				documentsPath = Application.persistentDataPath + "/Documents";
			}
			return documentsPath;
		}
	}

	public static GameObject[] GetGameObjectInRaycastHit(params RaycastHit[] hits)
	{
		if (hits == null)
		{
			return null;
		}
		GameObject[] array = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; i++)
		{
			array[i] = hits[i].transform.gameObject;
		}
		return array;
	}

	public static float AngleToRad(float angle)
	{
		return angle / 180f * (float)Math.PI;
	}

	public static float RadToAngle(float rad)
	{
		return rad / (float)Math.PI * 180f;
	}

	public static int Random(int min, int max)
	{
		if (random == null)
		{
			long ticks = DateTime.Now.Ticks;
			random = new System.Random((int)(ticks & 0xFFFFFFFFu) | (int)(ticks >> 32));
		}
		return random.Next(min, max);
	}

	public static float Random01()
	{
		if (random == null)
		{
			long ticks = DateTime.Now.Ticks;
			random = new System.Random((int)(ticks & 0xFFFFFFFFu) | (int)(ticks >> 32));
		}
		return (float)random.NextDouble();
	}

	public static List<T> RandomSortList<T>(List<T> list)
	{
		List<T> list2 = new List<T>();
		List<T> list3 = new List<T>();
		list2.AddRange(list);
		while (list2.Count > 0)
		{
			list3.Add(list2[Random(0, list2.Count)]);
			list2.Remove(list3[list3.Count - 1]);
		}
		return list3;
	}

	public static int GetDevicePerformance()
	{
		int num = 100;
		return 100;
	}

	public static string Time2Str(long t)
	{
		long num = t / 3600000;
		long num2 = (t - num * 3600000) / 60000;
		return string.Concat(str2: ((t - num * 3600000 - num2 * 60000) / 1000).ToString("d2"), str0: num2.ToString("d2"), str1: ":");
	}
}
