using UnityEngine;

public class SaveHandle : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			Player.Instance.Save(true);
		}
	}
}
