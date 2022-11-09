using UnityEngine;

public class MoneyMono : MonoBehaviour
{
	public float min;

	public float max;

	public new ITAudioEvent audio;

	private bool isUsed;

	private void OnTriggerEnter(Collider other)
	{
		if (!isUsed && other.tag == "Hero")
		{
			audio.Trigger();
			audio.transform.parent = null;
			other.GetComponent<Hero>().AddGold(Random.Range(min, max));
			isUsed = true;
			Object.Destroy(base.gameObject);
		}
	}
}
