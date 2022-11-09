using System.Collections.Generic;
using Event;
using Fight;
using UnityEngine;

[RequireComponent(typeof(WeaponIntervalControl))]
[RequireComponent(typeof(WeaponInputJudgment))]
public class Gun : AWeapon<GunData>
{
	public Transform muzzle;

	public Transform flameLightPoint;

	protected WeaponIntervalControl intervalControl;

	protected WeaponInputJudgment inputJudgment;

	protected BulletCaseEmitter bulletCaseEmitter;

	protected FlameEmitter flameEmitter;

	protected FlameLightEffect flameLightEffect;

	public int Bullets { get; protected set; }

	public override IWeaponInputJudgment Judgment
	{
		get
		{
			if (null == inputJudgment)
			{
				inputJudgment = GetComponent<WeaponInputJudgment>();
			}
			return inputJudgment;
		}
	}

	protected override void DoAttack()
	{
		if (0 < Bullets)
		{
			Ray ray = new Ray(muzzle.position, muzzle.forward);
			List<GameObject> list = new List<GameObject>();
			RaycastHit hitInfo;
			if (base.Data.penetrable)
			{
				RaycastHit[] hits = Physics.RaycastAll(ray, base.Data.attackRange, base.AttackLayerMask);
				GameObject[] gameObjectInRaycastHit = ZombieStreetCommon.GetGameObjectInRaycastHit(hits);
				if (gameObjectInRaycastHit != null)
				{
					for (int i = 0; i < gameObjectInRaycastHit.Length && i < base.Data.penetrableNumber + 1; i++)
					{
						list.Add(gameObjectInRaycastHit[i]);
					}
				}
			}
			else if (Physics.Raycast(ray, out hitInfo, base.Data.attackRange, base.AttackLayerMask))
			{
				GameObject[] gameObjectInRaycastHit2 = ZombieStreetCommon.GetGameObjectInRaycastHit(hitInfo);
				list.Add(gameObjectInRaycastHit2[0]);
			}
			if (list.Count > 0)
			{
				FightManager.Instance.Add(new ShootFightBehavior(this, base.Owner, list.ToArray()));
			}
			if (null != flameEmitter)
			{
				flameEmitter.Emit();
			}
			if (null != bulletCaseEmitter)
			{
				bulletCaseEmitter.Emit();
			}
			if (null != flameLightEffect)
			{
				flameLightEffect.Emit(flameLightPoint);
			}
			SubBullet(1);
		}
		intervalControl.BeginInterval();
	}

	private void Awake()
	{
		inputJudgment = GetComponent<WeaponInputJudgment>();
		bulletCaseEmitter = GetComponent<BulletCaseEmitter>();
		flameEmitter = GetComponent<FlameEmitter>();
		flameLightEffect = GetComponent<FlameLightEffect>();
		intervalControl = GetComponent<WeaponIntervalControl>();
		intervalControl.AddBeginIntervalHandle(WhenBeginInterval);
		intervalControl.AddEndIntervalHandle(WhenEndInterval);
	}

	protected override void DoInitialize()
	{
		intervalControl.Interval = base.Data.interval;
		Bullets = base.Data.maxOfBullets;
	}

	protected virtual void WhenBeginInterval()
	{
		Judgment.Lock();
	}

	protected virtual void WhenEndInterval()
	{
		Judgment.Unlock();
	}

	public override void OnRemove()
	{
		if (null != bulletCaseEmitter)
		{
			bulletCaseEmitter.Clear();
		}
		inputJudgment.Reset();
	}

	public virtual void AddBullet(float rate)
	{
		Bullets += (int)((float)base.Data.maxOfBullets * rate);
		if (Bullets > base.Data.maxOfBullets)
		{
			Bullets = base.Data.maxOfBullets;
		}
	}

	public virtual void SubBullet(int count)
	{
		Bullets -= count;
		if (0 > Bullets)
		{
			Bullets = 0;
		}
		EventCenter.Instance.Publish(this, new UseGunEvent(base.Data.id));
		PublishBulletCountChangeEvent();
	}

	public void PublishBulletCountChangeEvent()
	{
		EventCenter.Instance.Publish(null, new BulletCountChangeEvent(Bullets, base.Data.maxOfBullets));
	}
}
