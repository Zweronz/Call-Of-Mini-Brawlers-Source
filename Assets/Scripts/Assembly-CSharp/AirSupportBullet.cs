using Fight;
using UnityEngine;

public class AirSupportBullet : MonoBehaviour
{
	public AirSupport airSupport;

	public GameObject prefab;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Ground"))
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefab, base.transform.position, Quaternion.identity);
			gameObject.AddComponent<AutoDestroyWhenNoChild>();
			GameObject[] victims = GameObject.FindGameObjectsWithTag("Zombie");
			FightManager.Instance.Add(new AirSupportFightBehavior(airSupport, victims));
			base.gameObject.SetActiveRecursively(false);
			Object.Destroy(base.gameObject);
		}
	}
}
