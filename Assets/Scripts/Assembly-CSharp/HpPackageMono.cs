using UnityEngine;

public class HpPackageMono : MonoBehaviour
{
	public HpMedicine package;

	public new ITAudioEvent audio;

	private bool isUsed;

	private void OnTriggerEnter(Collider other)
	{
		if (!isUsed && other.tag == "Hero")
		{
			audio.Trigger();
			audio.transform.parent = null;
			package.Use(other.GetComponent<Hero>());
			isUsed = true;
			Object.Destroy(base.gameObject);
		}
	}
}
