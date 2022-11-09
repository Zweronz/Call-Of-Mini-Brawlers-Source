using UnityEngine;

public class SniperRifle : Gun
{
	private LaserEmitter laserEmitter;

	protected override void DoAttack()
	{
	}

	protected override void DoInitialize()
	{
		base.DoInitialize();
		laserEmitter = GetComponent<LaserEmitter>();
		(inputJudgment as SniperRifleInputJudgment).waittingTime = base.Data.interval;
		intervalControl.Interval = 0f;
	}

	private void RealShoot()
	{
		base.DoAttack();
		if (null != base.Owner)
		{
			base.Owner.SendMessage("SniperRifleRealShoot", SendMessageOptions.DontRequireReceiver);
		}
		CloseLaser();
		Judgment.Reset();
	}

	private void CancelShoot()
	{
		if (null != base.Owner)
		{
			base.Owner.SendMessage("SniperRifleCancelShoot", SendMessageOptions.DontRequireReceiver);
		}
		CloseLaser();
	}

	private void WaittingOver()
	{
		if (null != laserEmitter)
		{
			laserEmitter.Emit();
		}
	}

	private void CloseLaser()
	{
		if (null != laserEmitter)
		{
			laserEmitter.Stop();
		}
	}
}
