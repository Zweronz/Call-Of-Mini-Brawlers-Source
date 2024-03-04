using System.Collections.Generic;
using UnityEngine;

public class PerformanceColliderControl : MonoBehaviour
{
	private void Start()
	{
		List<Collider> list = new List<Collider>();
		Collider[] componentsInChildren = base.gameObject.GetComponentsInChildren<Collider>(true);
		if (componentsInChildren == null || componentsInChildren.Length <= 0)
		{
			return;
		}
		list.Clear();
		list.AddRange(componentsInChildren);
		list.RemoveAll((Collider collider) => collider.tag != "SceneCollider");
		bool flag = ZombieStreetCommon.GetDevicePerformance() > ZombieStreetCommon.LowPerformance;
		foreach (Collider item in list)
		{
			item.enabled = flag;
		}
	}
}
