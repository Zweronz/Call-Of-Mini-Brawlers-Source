using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PerformanceColliderControl : MonoBehaviour
{
	[CompilerGenerated]
	private static Predicate<Collider> _003C_003Ef__am_0024cache0;

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
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = _003CStart_003Em__1;
		}
		list.RemoveAll(_003C_003Ef__am_0024cache0);
		bool flag = ZombieStreetCommon.GetDevicePerformance() > ZombieStreetCommon.LowPerformance;
		foreach (Collider item in list)
		{
			item.enabled = flag;
		}
	}

	[CompilerGenerated]
	private static bool _003CStart_003Em__1(Collider collider)
	{
		return collider.tag != "SceneCollider";
	}
}
