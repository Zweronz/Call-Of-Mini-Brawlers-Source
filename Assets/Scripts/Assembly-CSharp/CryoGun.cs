public class CryoGun : Gun
{
	public IceBulletEmitter emitter;

	public ITAudioEvent fire;

	protected override void DoAttack()
	{
		if (0 < base.Bullets)
		{
			emitter.Emit();
			fire.Trigger();
			if (null != flameEmitter)
			{
				flameEmitter.Emit();
			}
			SubBullet(1);
		}
		intervalControl.BeginInterval();
	}

	protected override void DoInitialize()
	{
		base.DoInitialize();
		emitter = GetComponent<IceBulletEmitter>();
	}

	public override void AddBullet(float rate)
	{
		bool flag = 0 == base.Bullets;
		base.AddBullet(rate);
	}
}
