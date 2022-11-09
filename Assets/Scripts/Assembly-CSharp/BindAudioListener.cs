using UnityEngine;

public class BindAudioListener : MonoBehaviour
{
	private void Update()
	{
		TAudioManager.instance.AudioListener.transform.position = base.transform.position;
	}
}
