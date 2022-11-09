using UnityEngine;

public class Tool
{
	public static bool InArea(GameObject source, GameObject tag, float radius)
	{
		if (null != tag && Mathf.Abs((tag.transform.position - source.transform.position).z) <= radius)
		{
			return true;
		}
		return false;
	}
}
