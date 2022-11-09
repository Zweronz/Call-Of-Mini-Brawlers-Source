using UnityEngine;

public class LaserGun : Gun
{
	private LaserEmitter laserEmitter;

	private bool isInterval;

	public Animation anim;

	public AnimationClip attackAnim;

	public AnimationClip stopAnim;

	public ITAudioEvent fire;

	public ITAudioEvent loop;

	protected override void DoAttack()
	{
		if (0 < base.Bullets)
		{
			StartAttackAnim();
			if (!isInterval)
			{
				OpenLaser();
				fire.Trigger();
				loop.Stop();
				base.DoAttack();
			}
		}
	}

	protected override void DoInitialize()
	{
		base.DoInitialize();
		laserEmitter = GetComponent<LaserEmitter>();
	}

	protected virtual void StopAttackAnim()
	{
		if (anim.IsPlaying(attackAnim.name) && anim[attackAnim.name].weight > 0f)
		{
			anim.CrossFade(stopAnim.name);
		}
	}

	protected virtual void StartAttackAnim()
	{
		anim.Play(attackAnim.name);
	}

	private void CancelShoot()
	{
		if (null != base.Owner)
		{
			base.Owner.SendMessage("OnLaserGunAttackEnd", SendMessageOptions.DontRequireReceiver);
		}
		CloseLaser();
		loop.Stop();
		StopAttackAnim();
	}

	private void OpenLaser()
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

	public override void OnRemove()
	{
		base.OnRemove();
		CloseLaser();
		loop.Stop();
		StopAttackAnim();
	}

	public override void SubBullet(int count)
	{
		base.SubBullet(count);
		if (base.Bullets <= 0)
		{
			CancelShoot();
		}
	}

	protected override void WhenBeginInterval()
	{
		isInterval = true;
	}

	protected override void WhenEndInterval()
	{
		isInterval = false;
	}
}
