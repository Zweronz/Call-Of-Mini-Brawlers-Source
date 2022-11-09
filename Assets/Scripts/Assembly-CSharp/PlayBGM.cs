using UnityEngine;

public class PlayBGM : MonoBehaviour
{
	public ITAudioEvent bgm;

	private void Start()
	{
		if (null != bgm)
		{
			bgm.transform.position = base.transform.position;
			bgm.Trigger();
		}
	}
}
