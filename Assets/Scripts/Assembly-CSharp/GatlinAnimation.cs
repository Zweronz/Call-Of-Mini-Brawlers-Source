using UnityEngine;

public class GatlinAnimation : WeaponAnimation
{
	public AnimationClip standUp;

	public AnimationClip standDown;

	public AnimationClip moveUp;

	public AnimationClip moveDown;

	public AnimationClip attackAnim;

	private bool isRunning;

	private bool isMixed;

	private AnimationClip regressionUp;

	private AnimationClip regressionDown;

	private AnimationClip attackWaittingAnim;

	public override void BeEnable()
	{
		base.BeEnable();
		anim[attackAnim.name].wrapMode = WrapMode.ClampForever;
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[attackAnim.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = anim[attackAnim.name].length;
		animationTriggerEvent.functionName = "OnGatlinAttackEnd";
		animationTriggerEvent.AddToClip();
		anim[standUp.name].wrapMode = WrapMode.Loop;
		anim[standDown.name].wrapMode = WrapMode.Loop;
		anim[moveUp.name].wrapMode = WrapMode.Loop;
		anim[moveDown.name].wrapMode = WrapMode.Loop;
	}

	public override void BeDisable()
	{
		base.BeDisable();
		StopAttackAnim();
	}

	public override void PlayStandAnimation(float fadeLength = 0.3f)
	{
		isRunning = false;
		regressionUp = standUp;
		regressionDown = standDown;
		MoveToStand();
		anim[standUp.name].time = anim[standDown.name].time;
		anim.CrossFade(standUp.name, fadeLength);
		anim.CrossFade(standDown.name, fadeLength);
	}

	public override void PlayMoveAnimation()
	{
		isRunning = true;
		regressionUp = moveUp;
		regressionDown = moveDown;
		StandToMove();
		anim[moveUp.name].time = anim[moveDown.name].time;
		anim.CrossFade(moveUp.name);
		anim.CrossFade(moveDown.name);
	}

	public override void PlayAttackAnimation()
	{
		StopAttackAnim();
		if (isRunning)
		{
			StandToMove();
		}
		else
		{
			MoveToStand();
		}
		anim[attackAnim.name].layer = 10;
		anim[attackAnim.name].weight = 1f;
		anim.CrossFade(attackAnim.name);
		attackWaittingAnim = attackAnim;
	}

	public override void OnChangeMoveSpeed(float speed)
	{
		AnimationState animationState = anim[moveUp.name];
		animationState.speed = speed;
		animationState = anim[moveDown.name];
		animationState.speed = speed;
	}

	public override void ImproveAttackAnimationSpeed(float interval)
	{
		AnimationState animationState = anim[attackAnim.name];
		float speed = 1f;
		if (interval > 0f)
		{
			speed = animationState.clip.length / interval;
		}
		animationState.speed = speed;
	}

	protected virtual void OnGatlinAttackEnd()
	{
		if (isEnabled)
		{
			Regression();
		}
	}

	protected virtual void Regression()
	{
		anim[regressionUp.name].weight = 0f;
		anim[attackWaittingAnim.name].weight = 1f;
		anim[attackWaittingAnim.name].layer = anim[regressionUp.name].layer;
		anim.Stop(regressionUp.name);
		anim[regressionUp.name].time = anim[regressionDown.name].time;
		anim.CrossFade(regressionUp.name);
	}

	protected virtual void StopAttackAnim()
	{
		anim.Stop(attackAnim.name);
	}

	protected virtual void StandToMove()
	{
		if (!isMixed)
		{
			anim[attackAnim.name].AddMixingTransform(boneFinder.Query("Waist").bone);
		}
	}

	protected virtual void MoveToStand()
	{
		if (isMixed)
		{
			anim[attackAnim.name].RemoveMixingTransform(boneFinder.Query("Waist").bone);
		}
	}

	protected override void DoOnAvoidOver()
	{
		anim[avoid.name].layer = 5;
		anim[avoid.name].weight = 1f;
		anim[standUp.name].weight = 0f;
		anim[standDown.name].weight = 0f;
		anim[moveUp.name].weight = 0f;
		anim[moveDown.name].weight = 0f;
		PlayStandAnimation();
	}

	protected override void StopAllAnim()
	{
		anim.Stop(standUp.name);
		anim.Stop(standDown.name);
		anim.Stop(moveUp.name);
		anim.Stop(moveDown.name);
		anim.Stop(avoid.name);
		anim.Stop(attackAnim.name);
	}
}
