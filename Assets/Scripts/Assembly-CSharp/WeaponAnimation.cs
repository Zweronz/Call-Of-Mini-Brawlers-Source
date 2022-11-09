using Event;
using UnityEngine;

public abstract class WeaponAnimation : MonoBehaviour, IWeaponAnimation
{
	public string subTitle;

	public AnimationClip deadAnim;

	public AnimationClip avoid;

	protected Animation anim;

	protected BoneFinder boneFinder;

	protected bool isEnabled;

	public virtual void Bind(Animation anim, BoneFinder boneFinder)
	{
		this.anim = anim;
		this.boneFinder = boneFinder;
	}

	public virtual void BeEnable()
	{
		isEnabled = true;
		anim[deadAnim.name].wrapMode = WrapMode.ClampForever;
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[deadAnim.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = anim[deadAnim.name].length;
		animationTriggerEvent.functionName = "OnDeadOver";
		animationTriggerEvent.data = subTitle;
		animationTriggerEvent.AddToClip();
		anim[avoid.name].wrapMode = WrapMode.ClampForever;
		animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[avoid.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = 17f / anim[avoid.name].clip.frameRate;
		animationTriggerEvent.functionName = "OnAvoidOver";
		animationTriggerEvent.data = subTitle;
		animationTriggerEvent.AddToClip();
	}

	public virtual void BeDisable()
	{
		isEnabled = false;
	}

	public abstract void PlayStandAnimation(float fadeLength = 0.3f);

	public abstract void PlayMoveAnimation();

	public abstract void PlayAttackAnimation();

	public virtual void PlayAvoidAnimation()
	{
		StopAllAnim();
		anim[avoid.name].layer = 11;
		anim[avoid.name].weight = 1f;
		anim.Play(avoid.name);
	}

	public virtual void PlayDeadAnimation()
	{
		anim.CrossFade(deadAnim.name);
	}

	public virtual void PlayReviveAnimation()
	{
		anim.Stop(deadAnim.name);
		PlayStandAnimation();
	}

	public abstract void OnChangeMoveSpeed(float speed);

	public abstract void ImproveAttackAnimationSpeed(float attackSpeed);

	protected abstract void StopAllAnim();

	protected virtual void OnDeadOver(string subTitle)
	{
		if (this.subTitle.Equals(subTitle))
		{
			EventCenter.Instance.Publish(this, new HeroDeadEvent());
		}
	}

	protected void OnAvoidOver(string subTitle)
	{
		if (this.subTitle.Equals(subTitle))
		{
			DoOnAvoidOver();
			EventCenter.Instance.Publish(this, new AvoidOverEvent());
		}
	}

	protected abstract void DoOnAvoidOver();
}
