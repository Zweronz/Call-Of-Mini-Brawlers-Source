using UnityEngine;

public class RPGAnimation : WeaponAnimation
{
	public GameObject notiftyObj;

	public AnimationClip standUp;

	public AnimationClip standDown;

	public AnimationClip moveUp;

	public AnimationClip moveDown;

	public AnimationClip attackAnim;

	public AnimationClip standAttackAnim;

	private AnimationClip regressionUp;

	private AnimationClip regressionDown;

	private AnimationClip attackWaittingAnim;

	private bool isLock;

	private bool isRunning;

	public override void BeEnable()
	{
		base.BeEnable();
		isLock = false;
		anim[attackAnim.name].wrapMode = WrapMode.ClampForever;
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[attackAnim.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = anim[attackAnim.name].length;
		animationTriggerEvent.functionName = "OnRPGAttackEnd";
		animationTriggerEvent.AddToClip();
		animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[standAttackAnim.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = anim[standAttackAnim.name].length;
		animationTriggerEvent.functionName = "OnRPGAttackEnd";
		animationTriggerEvent.AddToClip();
		anim[standUp.name].wrapMode = WrapMode.Loop;
		anim[standDown.name].wrapMode = WrapMode.Loop;
		anim[moveUp.name].wrapMode = WrapMode.Loop;
		anim[moveDown.name].wrapMode = WrapMode.Loop;
	}

	public override void BeDisable()
	{
		base.BeDisable();
		StopAllAnim();
		UnlockMoveWhenAttackEnd();
	}

	public override void PlayStandAnimation(float fadeLength = 0.3f)
	{
		isRunning = false;
		regressionUp = standUp;
		regressionDown = standDown;
		anim[standUp.name].time = anim[standDown.name].time;
		anim.CrossFade(standUp.name, fadeLength);
		anim.CrossFade(standDown.name, fadeLength);
	}

	public override void PlayMoveAnimation()
	{
		isRunning = true;
		regressionUp = moveUp;
		regressionDown = moveDown;
		MovePretreatment();
		anim[moveUp.name].time = anim[moveDown.name].time;
		anim.CrossFade(moveUp.name);
		anim.CrossFade(moveDown.name);
	}

	public override void PlayAttackAnimation()
	{
		LockMoveWhenAttack();
		StopAttackAnim();
		if (isRunning)
		{
			anim[attackAnim.name].layer = 10;
			anim[attackAnim.name].weight = 1f;
			anim.CrossFade(attackAnim.name);
			attackWaittingAnim = attackAnim;
		}
		else
		{
			anim[standAttackAnim.name].layer = 10;
			anim[standAttackAnim.name].weight = 1f;
			anim.CrossFade(standAttackAnim.name);
			attackWaittingAnim = standAttackAnim;
		}
	}

	public override void PlayAvoidAnimation()
	{
		UnlockMoveWhenAttackEnd();
		base.PlayAvoidAnimation();
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

	protected virtual void OnRPGAttackEnd()
	{
		if (isEnabled)
		{
			UnlockMoveWhenAttackEnd();
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

	protected virtual void LockMoveWhenAttack()
	{
		notiftyObj.SendMessage("LockLookAtNearestEnemy", SendMessageOptions.DontRequireReceiver);
		notiftyObj.SendMessage("OnStand", SendMessageOptions.DontRequireReceiver);
		isLock = true;
	}

	protected virtual void UnlockMoveWhenAttackEnd()
	{
		if (isLock)
		{
			notiftyObj.SendMessage("UnlockLookAtNearestEnemy", SendMessageOptions.DontRequireReceiver);
			isLock = false;
		}
	}

	protected override void DoOnAvoidOver()
	{
		if (isEnabled)
		{
			anim[avoid.name].layer = 5;
			anim[avoid.name].weight = 1f;
			anim[standUp.name].weight = 0f;
			anim[standDown.name].weight = 0f;
			anim[moveUp.name].weight = 0f;
			anim[moveDown.name].weight = 0f;
			PlayStandAnimation();
		}
	}

	protected override void StopAllAnim()
	{
		anim.Stop(standUp.name);
		anim.Stop(standDown.name);
		anim.Stop(moveUp.name);
		anim.Stop(moveDown.name);
		anim.Stop(avoid.name);
		anim.Stop(attackAnim.name);
		anim.Stop(standAttackAnim.name);
	}

	protected virtual void MovePretreatment()
	{
		if (anim.IsPlaying(standAttackAnim.name))
		{
			StandToMove();
		}
	}

	protected virtual void StandToMove()
	{
		anim[attackAnim.name].layer = anim[standAttackAnim.name].layer;
		anim[attackAnim.name].weight = anim[standAttackAnim.name].weight;
		anim[attackAnim.name].time = anim[standAttackAnim.name].time;
		anim.Stop(standAttackAnim.name);
		anim.Play(attackAnim.name);
		attackWaittingAnim = attackAnim;
	}

	protected virtual void StopAttackAnim()
	{
		anim.Stop(attackAnim.name);
		anim.Stop(standAttackAnim.name);
	}
}
