using UnityEngine;

public class RPG : Gun
{
	public GameObject grenadePrefab;

	public Transform grenadePoint;

	private Grenade currentGrenade;

	protected override void DoAttack()
	{
		if (0 < base.Bullets)
		{
			if (null != currentGrenade)
			{
				currentGrenade.Fly(grenadePoint.position, muzzle.rotation);
			}
			intervalControl.BeginInterval();
			currentGrenade = null;
			SubBullet(1);
		}
	}

	public void Reload()
	{
		if (0 < base.Bullets && null == currentGrenade)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(grenadePrefab);
			gameObject.transform.parent = grenadePoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			currentGrenade = gameObject.GetComponent<Grenade>();
			currentGrenade.owner = this;
		}
	}

	protected override void DoInitialize()
	{
		base.DoInitialize();
		intervalControl.AddEndIntervalHandle(ReloadWhenEndInterval);
		Reload();
	}

	private void ReloadWhenEndInterval()
	{
		Reload();
	}

	public override void AddBullet(float rate)
	{
		bool flag = 0 == base.Bullets;
		base.AddBullet(rate);
		if (flag)
		{
			Reload();
		}
	}
}
