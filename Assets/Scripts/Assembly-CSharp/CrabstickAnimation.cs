using System;
using System.Collections.Generic;
using UnityEngine;

public class CrabstickAnimation : WeaponAnimation
{
	public GameObject notiftyObj;

	public AnimationClip standUp;

	public AnimationClip standDown;

	public AnimationClip moveUp;

	public AnimationClip moveDown;

	public List<AnimationClip> attackAnims;

	private AnimationClip regressionUp;

	private AnimationClip regressionDown;

	private AnimationClip currentAttackAnim;

	private int currentAttackAnimIndex;

	private bool isLock;

	private bool isRunning;

	private bool isMixed;

	private readonly string linkString = "_&_";

	public override void BeEnable()
	{
		base.BeEnable();
		isLock = false;
		anim[standUp.name].wrapMode = WrapMode.Loop;
		anim[standDown.name].wrapMode = WrapMode.Loop;
		anim[moveUp.name].wrapMode = WrapMode.Loop;
		anim[moveDown.name].wrapMode = WrapMode.Loop;
		foreach (AnimationClip attackAnim in attackAnims)
		{
			anim[attackAnim.name].wrapMode = WrapMode.ClampForever;
			AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
			animationTriggerEvent.animationState = anim[attackAnim.name];
			animationTriggerEvent.obj = base.gameObject;
			animationTriggerEvent.time = anim[attackAnim.name].length;
			animationTriggerEvent.functionName = "OnCrabstickAttackEnd";
			animationTriggerEvent.data = subTitle + linkString + attackAnim.name;
			animationTriggerEvent.AddToClip();
		}
	}

	public override void BeDisable()
	{
		base.BeDisable();
		StopAttackAnim();
		UnlockMoveWhenAttackEnd();
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
		AnimationClip attackAnim = GetAttackAnim();
		if (attackAnim == currentAttackAnim)
		{
			anim.Stop(currentAttackAnim.name);
		}
		anim[attackAnim.name].layer = 10;
		anim[attackAnim.name].weight = 1f;
		if (isRunning)
		{
			StandToMove();
		}
		else
		{
			MoveToStand();
		}
		anim.CrossFade(attackAnim.name);
		currentAttackAnim = attackAnim;
	}

	public override void OnChangeMoveSpeed(float speed)
	{
		AnimationState animationState = anim[moveUp.name];
		animationState.speed = speed;
		animationState = anim[moveDown.name];
		animationState.speed = speed;
	}

	public override void ImproveAttackAnimationSpeed(float attackSpeed)
	{
		throw new NotImplementedException();
	}

	protected virtual void OnCrabstickAttackEnd(string clipName)
	{
		string[] array = clipName.Split(new string[1] { linkString }, StringSplitOptions.RemoveEmptyEntries);
		if (array[0].Equals(subTitle) && array[1] == currentAttackAnim.name)
		{
			Regression();
			UnlockMoveWhenAttackEnd();
		}
	}

	public override void PlayAvoidAnimation()
	{
		UnlockMoveWhenAttackEnd();
		base.PlayAvoidAnimation();
	}

	protected virtual void Regression()
	{
		anim[regressionUp.name].weight = 0f;
		anim[currentAttackAnim.name].weight = 1f;
		anim[currentAttackAnim.name].layer = anim[regressionUp.name].layer;
		anim.Stop(regressionUp.name);
		anim[regressionUp.name].time = anim[regressionDown.name].time;
		anim.CrossFade(regressionUp.name);
	}

	protected virtual void StandToMove()
	{
		if (isMixed)
		{
			return;
		}
		foreach (AnimationClip attackAnim in attackAnims)
		{
			anim[attackAnim.name].AddMixingTransform(boneFinder.Query("Waist").bone);
		}
		isMixed = true;
	}

	protected virtual void MoveToStand()
	{
		if (!isMixed)
		{
			return;
		}
		foreach (AnimationClip attackAnim in attackAnims)
		{
			anim[attackAnim.name].RemoveMixingTransform(boneFinder.Query("Waist").bone);
		}
		isMixed = false;
	}

	protected virtual void StopAttackAnim()
	{
		if (null != currentAttackAnim)
		{
			anim.Stop(currentAttackAnim.name);
		}
	}

	protected virtual AnimationClip GetAttackAnim()
	{
		if (currentAttackAnimIndex >= attackAnims.Count)
		{
			currentAttackAnimIndex = 0;
		}
		return attackAnims[currentAttackAnimIndex++];
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
		foreach (AnimationClip attackAnim in attackAnims)
		{
			anim.Stop(attackAnim.name);
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
}
