using System.Collections.Generic;
using Event;
using Fight;
using UnityEngine;

[RequireComponent(typeof(WeaponInputJudgment))]
[RequireComponent(typeof(WeaponIntervalControl))]
public class MeleeWeapon : AWeapon<MeleeWeaponData>
{
	public Transform muzzle;

	public FlameEmitter flameEmitter;

	protected WeaponIntervalControl intervalControl;

	protected WeaponInputJudgment inputJudgment;

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

	public void RealAttack()
	{
		Ray ray = new Ray(muzzle.position, muzzle.forward);
		List<GameObject> list = new List<GameObject>();
		RaycastHit[] hits = Physics.RaycastAll(ray, base.Data.attackRange, base.AttackLayerMask);
		GameObject[] gameObjectInRaycastHit = ZombieStreetCommon.GetGameObjectInRaycastHit(hits);
		if (gameObjectInRaycastHit != null)
		{
			list.AddRange(gameObjectInRaycastHit);
		}
		if (list.Count > 0)
		{
			FightManager.Instance.Add(new MeleeFightBehavior(this, base.Owner, list.ToArray()));
		}
		if (flameEmitter != null)
		{
			flameEmitter.Emit();
		}
		EventCenter.Instance.Publish(this, new UseMeleeWeaponEvent(base.Data.id));
	}

	protected override void DoAttack()
	{
		intervalControl.BeginInterval();
	}

	private void Awake()
	{
		inputJudgment = GetComponent<WeaponInputJudgment>();
		intervalControl = GetComponent<WeaponIntervalControl>();
		intervalControl.AddBeginIntervalHandle(WhenBeginInterval);
		intervalControl.AddEndIntervalHandle(WhenEndInterval);
	}

	protected override void DoInitialize()
	{
		intervalControl.Interval = base.Data.interval;
	}

	private void WhenBeginInterval()
	{
		Judgment.Lock();
	}

	private void WhenEndInterval()
	{
		Judgment.Unlock();
	}

	public override void OnEquip()
	{
		base.OnEquip();
	}
}
