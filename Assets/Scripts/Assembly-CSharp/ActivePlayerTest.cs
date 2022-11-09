using UnityEngine;

public class ActivePlayerTest : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			Player.Instance.UpdateActive();
		}
	}
}
