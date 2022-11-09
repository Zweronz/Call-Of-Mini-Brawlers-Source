using System.Collections.Generic;
using UnityEngine;

public class LocalNotification : MonoBehaviour
{
	private const string notifyDayDataKey = "notifyDay";

	public string m_alterAction;

	public List<string> m_alterBodys;

	public static int notifyDay;
}
