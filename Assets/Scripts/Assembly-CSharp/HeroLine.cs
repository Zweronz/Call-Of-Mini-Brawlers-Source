using UnityEngine;

public class HeroLine : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(base.transform.position, base.transform.forward * 1000f);
	}
}
