using System;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEffectModel : MonoBehaviour
{
	[Serializable]
	public class AppearEffectData
	{
		public int appearType;

		public GameObject effectPrefab;

		public Transform point;
	}

	public Transform hurtPoint;

	public List<GameObject> hurtEffectPrefabs;

	public GameObject bluntHurtEffectPrefab;

	public GameObject sharpHurtEffectPrefab;

	public List<GameObject> deadPrefabs;

	public List<GameObject> meleeDeadPrefabs;

	public Transform deadPoint;

	[SerializeField]
	protected List<AppearEffectData> appearEffects;

	[SerializeField]
	protected GameObject laserHurtPrefab;

	[SerializeField]
	protected Transform laserHurtPoint;

	[SerializeField]
	protected GameObject laserDeadPrefab;

	[SerializeField]
	protected GameObject iceFrozenPrefab;

	[SerializeField]
	protected GameObject frozenDeadPrefab;

	[SerializeField]
	protected float frozenTime;

	private GameObject iceFrozenObj;

	private Action onFrozenOver;

	private ZombieStreetTimer.TimerData timerData;

	private GameObject laserHurtObj;

	public void OnHurt()
	{
		if (hurtEffectPrefabs == null)
		{
			return;
		}
		foreach (GameObject hurtEffectPrefab in hurtEffectPrefabs)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(hurtEffectPrefab);
			gameObject.transform.parent = hurtPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnLaserHurt()
	{
		if (null == laserHurtObj && null != laserHurtPrefab)
		{
			laserHurtObj = (GameObject)UnityEngine.Object.Instantiate(laserHurtPrefab, laserHurtPoint.position, Quaternion.identity);
			laserHurtObj.transform.parent = laserHurtPoint;
			laserHurtObj.transform.localPosition = Vector3.zero;
			laserHurtObj.transform.localRotation = Quaternion.identity;
		}
		laserHurtObj.SetActiveRecursively(true);
	}

	public void OnLaserDead()
	{
		if (null != laserDeadPrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(laserDeadPrefab, laserHurtPoint.position, Quaternion.identity);
		}
	}

	public void CloseLaserHurt()
	{
		if (null != laserHurtObj)
		{
			laserHurtObj.SetActiveRecursively(false);
		}
	}

	public void OnSharpHurt()
	{
		if (null != sharpHurtEffectPrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(sharpHurtEffectPrefab);
			gameObject.transform.parent = hurtPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnBluntHurt()
	{
		if (null != bluntHurtEffectPrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(bluntHurtEffectPrefab);
			gameObject.transform.parent = hurtPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnDead()
	{
		int index = UnityEngine.Random.Range(0, deadPrefabs.Count);
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(deadPrefabs[index]);
		gameObject.transform.position = deadPoint.position;
		gameObject.transform.rotation = deadPoint.rotation;
	}

	public void OnFrozenDead()
	{
		if (null != frozenDeadPrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(frozenDeadPrefab, deadPoint.transform.position, Quaternion.identity);
			gameObject.transform.position = deadPoint.position;
			gameObject.transform.rotation = deadPoint.rotation;
		}
	}

	public void OnMeleeDead()
	{
		if (meleeDeadPrefabs == null || meleeDeadPrefabs.Count <= 0)
		{
			OnDead();
			return;
		}
		int index = UnityEngine.Random.Range(0, meleeDeadPrefabs.Count);
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(meleeDeadPrefabs[index]);
		gameObject.transform.position = deadPoint.position;
		gameObject.transform.rotation = deadPoint.rotation;
	}

	public void OnAppear(int appearType)
	{
		List<AppearEffectData> list = appearEffects.FindAll((AppearEffectData data) => appearType == data.appearType);
		if (list == null || list.Count <= 0)
		{
			return;
		}
		foreach (AppearEffectData item in list)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(item.effectPrefab, item.point.position, Quaternion.identity);
			gameObject.transform.position = item.point.position;
			gameObject.transform.rotation = item.point.rotation;
		}
	}

	public void OnFrozen(float time, Action onFrozenOver)
	{
		frozenTime = time;
		this.onFrozenOver = onFrozenOver;
		if (null == iceFrozenObj)
		{
			CreateIceFrozen();
		}
		else
		{
			UpdateFrozenTimer();
		}
	}

	private void CreateIceFrozen()
	{
		iceFrozenObj = (GameObject)UnityEngine.Object.Instantiate(iceFrozenPrefab);
		iceFrozenObj.transform.parent = deadPoint;
		iceFrozenObj.transform.localPosition = Vector3.zero;
		iceFrozenObj.transform.localRotation = Quaternion.identity;
		CreateFrozenTimer();
	}

	private void CreateFrozenTimer()
	{
		timerData = new ZombieStreetTimer.TimerData();
		timerData.invokeTimes = 1;
		timerData.time = frozenTime;
		timerData.handler = OnFrozenTimerOver;
		ZombieStreetTimer.Instance.AddTimer(timerData);
	}

	private void UpdateFrozenTimer()
	{
		if (timerData != null)
		{
			timerData.time = frozenTime;
		}
	}

	private void OnFrozenTimerOver(ZombieStreetTimer.TimerData data)
	{
		UnityEngine.Object.Destroy(iceFrozenObj);
		iceFrozenObj = null;
		ZombieStreetTimer.RemoveTimer(data);
		timerData = null;
		if (onFrozenOver != null)
		{
			onFrozenOver();
		}
	}

	private void OnDestroy()
	{
		if (timerData != null)
		{
			ZombieStreetTimer.RemoveTimer(timerData);
		}
	}
}
