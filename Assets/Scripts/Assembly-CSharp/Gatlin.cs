using UnityEngine;

[RequireComponent(typeof(WeaponIntervalControl))]
[RequireComponent(typeof(WeaponInputJudgment))]
public class Gatlin : Gun
{
	public Animation anim;

	public AnimationClip attackAnim;

	public AnimationClip stopAnim;

	public ITAudioEvent atk;

	public ITAudioEvent stop;

	protected override void DoAttack()
	{
		anim.CrossFade(attackAnim.name);
		if (!atk.isPlaying)
		{
			atk.Trigger();
		}
		base.DoAttack();
	}

	protected virtual void StopAttackAnim()
	{
		if (anim.IsPlaying(attackAnim.name) && anim[attackAnim.name].weight > 0f)
		{
			anim.CrossFade(stopAnim.name);
		}
		atk.Stop();
		stop.Trigger();
	}

	protected override void DoInitialize()
	{
		base.DoInitialize();
		anim[attackAnim.name].wrapMode = WrapMode.Loop;
		(inputJudgment as GatlinInputJudgment).del = StopAttackAnim;
	}

	public override void OnRemove()
	{
		anim.Stop(attackAnim.name);
		anim.Stop(stopAnim.name);
		atk.Stop();
	}
}
