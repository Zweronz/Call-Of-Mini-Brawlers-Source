using UnityEngine;

public class SniperRifleAnimation : WeaponAnimation
{
	public GameObject notiftyObj;

	public AnimationClip standUp;

	public AnimationClip standDown;

	public AnimationClip moveUp;

	public AnimationClip moveDown;

	public AnimationClip attackAnim;

	public AnimationClip attackWaittingAnim;

	public float waittingTime;

	private bool isWaitting;

	private float waittingTimer;

	private bool isAttacking;

	private bool isRunning;

	private bool isMixed;

	private AnimationClip regressionUp;

	private AnimationClip regressionDown;

	private bool isLock;

	public override void BeEnable()
	{
		base.BeEnable();
		isAttacking = false;
		isLock = false;
		anim[attackWaittingAnim.name].wrapMode = WrapMode.ClampForever;
		anim[attackAnim.name].wrapMode = WrapMode.ClampForever;
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[attackAnim.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = anim[attackAnim.name].length;
		animationTriggerEvent.functionName = "OnSniperRifleAttackEnd";
		animationTriggerEvent.data = subTitle;
		animationTriggerEvent.AddToClip();
		anim[standUp.name].wrapMode = WrapMode.Loop;
		anim[standDown.name].wrapMode = WrapMode.Loop;
		anim[moveUp.name].wrapMode = WrapMode.Loop;
		anim[moveDown.name].wrapMode = WrapMode.Loop;
	}

	public override void BeDisable()
	{
		base.BeDisable();
		StopWaitting();
		anim.Stop(attackAnim.name);
		anim.Stop(attackWaittingAnim.name);
		UnlockMoveWhenAttackEnd();
	}

	public override void PlayStandAnimation(float fadeLength = 0.3f)
	{
		isRunning = false;
		regressionUp = standUp;
		regressionDown = standDown;
		ChangeMovement();
		anim[standUp.name].time = anim[standDown.name].time;
		anim.CrossFade(standUp.name, fadeLength);
		anim.CrossFade(standDown.name, fadeLength);
	}

	public override void PlayMoveAnimation()
	{
		isRunning = true;
		regressionUp = moveUp;
		regressionDown = moveDown;
		ChangeMovement();
		anim[moveUp.name].time = anim[moveDown.name].time;
		anim.CrossFade(moveUp.name);
		anim.CrossFade(moveDown.name);
	}

	public override void PlayAttackAnimation()
	{
		if (!isAttacking)
		{
			PlayWaittingAnimation(0f);
		}
		StopWaitting();
		LockMoveWhenAttack();
		isAttacking = true;
	}

	public override void PlayAvoidAnimation()
	{
		isAttacking = false;
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

	protected virtual void OnSniperRifleAttackEnd(string subTitle)
	{
		if (base.subTitle.Equals(subTitle))
		{
			UnlockMoveWhenAttackEnd();
			anim.Stop(attackAnim.name);
			PlayWaittingAnimation(1f);
			if (!isAttacking)
			{
				BeginWaitting();
			}
		}
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

	protected virtual void StandToMove()
	{
		if (!isMixed)
		{
			anim[attackWaittingAnim.name].AddMixingTransform(boneFinder.Query("Waist").bone);
			anim[attackAnim.name].AddMixingTransform(boneFinder.Query("Waist").bone);
		}
	}

	protected virtual void MoveToStand()
	{
		if (isMixed)
		{
			anim[attackWaittingAnim.name].RemoveMixingTransform(boneFinder.Query("Waist").bone);
			anim[attackAnim.name].RemoveMixingTransform(boneFinder.Query("Waist").bone);
		}
	}

	protected virtual void ChangeMovement()
	{
		if (isRunning)
		{
			StandToMove();
		}
		else
		{
			MoveToStand();
		}
	}

	protected virtual void PlayWaittingAnimation(float weight)
	{
		anim.Stop(attackWaittingAnim.name);
		anim[attackWaittingAnim.name].layer = 10;
		anim[attackWaittingAnim.name].weight = weight;
		ChangeMovement();
		anim.CrossFade(attackWaittingAnim.name);
	}

	protected virtual void SniperRifleRealShoot()
	{
		anim.Stop(attackAnim.name);
		anim.Stop(attackWaittingAnim.name);
		anim[attackAnim.name].layer = 10;
		anim[attackAnim.name].weight = 1f;
		ChangeMovement();
		anim.CrossFade(attackAnim.name);
	}

	protected virtual void SniperRifleCancelShoot()
	{
		UnlockMoveWhenAttackEnd();
		if (isWaitting)
		{
			ResetWaittingTimer();
			return;
		}
		Regression();
		StopWaitting();
		isAttacking = false;
	}

	protected virtual void BeginWaitting()
	{
		ResetWaittingTimer();
		isWaitting = true;
		isAttacking = true;
	}

	protected virtual void StopWaitting()
	{
		ResetWaittingTimer();
		isWaitting = false;
	}

	protected virtual void ResetWaittingTimer()
	{
		waittingTimer = 0f;
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

	protected virtual void Update()
	{
		if (isWaitting)
		{
			waittingTimer += Time.deltaTime;
			if (waittingTimer >= waittingTime)
			{
				Regression();
				StopWaitting();
				isAttacking = false;
			}
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
		anim.Stop(attackWaittingAnim.name);
	}
}
