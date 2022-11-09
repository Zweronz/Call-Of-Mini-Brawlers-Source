using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombieAnimationModel : MonoBehaviour
{
	[Serializable]
	public class MoveAnimationData
	{
		public AnimationClip moveAnim;

		public float minSpeed;

		public float maxSpeed;
	}

	[Serializable]
	public class AppearAnimationData
	{
		public int appearType;

		public AnimationClip appearAnim;
	}

	[CompilerGenerated]
	private sealed class _003COnAppear_003Ec__AnonStorey2D
	{
		internal int appearType;

		internal bool _003C_003Em__51(AppearAnimationData data)
		{
			return appearType == data.appearType;
		}
	}

	public GameObject notifyObj;

	public string function;

	[SerializeField]
	protected Animation anim;

	[SerializeField]
	protected List<MoveAnimationData> moveAnims;

	public AnimationClip standAnim;

	public AnimationClip attackPreparationAnim;

	public float waittingTime;

	public AnimationClip attackAnim;

	public List<AnimationClip> hurtAnims;

	public AnimationClip meleeHurtAnim;

	[SerializeField]
	protected List<AppearAnimationData> appearAnims;

	[SerializeField]
	protected List<AnimationClip> laserHurtAnims;

	private AnimationClip currentMoveAnim;

	private bool isWaitting;

	private float waittingTimer;

	private bool isPause;

	private string currentLasrHurtAnim;

	private List<string> anims = new List<string>();

	private void Start()
	{
		if (hurtAnims != null && hurtAnims.Count > 0)
		{
			foreach (AnimationClip hurtAnim in hurtAnims)
			{
				AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
				animationTriggerEvent.animationState = anim[hurtAnim.name];
				animationTriggerEvent.obj = base.gameObject;
				animationTriggerEvent.time = anim[hurtAnim.name].length;
				animationTriggerEvent.functionName = "OnHurtAnimOver";
				animationTriggerEvent.AddToClip();
			}
		}
		if (null != meleeHurtAnim)
		{
			AnimationTriggerEvent animationTriggerEvent2 = new AnimationTriggerEvent();
			animationTriggerEvent2.animationState = anim[meleeHurtAnim.name];
			animationTriggerEvent2.obj = base.gameObject;
			animationTriggerEvent2.time = anim[meleeHurtAnim.name].length;
			animationTriggerEvent2.functionName = "OnHurtAnimOver";
			animationTriggerEvent2.AddToClip();
		}
		if (laserHurtAnims == null || laserHurtAnims.Count <= 0)
		{
			return;
		}
		foreach (AnimationClip laserHurtAnim in laserHurtAnims)
		{
			AnimationTriggerEvent animationTriggerEvent3 = new AnimationTriggerEvent();
			animationTriggerEvent3.animationState = anim[laserHurtAnim.name];
			animationTriggerEvent3.obj = base.gameObject;
			animationTriggerEvent3.time = anim[laserHurtAnim.name].length;
			animationTriggerEvent3.functionName = "OnHurtAnimOver";
			animationTriggerEvent3.AddToClip();
		}
	}

	public void OnMove()
	{
		if (!isPause)
		{
			anim[currentMoveAnim.name].wrapMode = WrapMode.Loop;
			anim.CrossFade(currentMoveAnim.name);
		}
	}

	public void SwitchMoveAnim(float speed)
	{
		if (moveAnims == null || moveAnims.Count <= 0)
		{
			return;
		}
		foreach (MoveAnimationData moveAnim in moveAnims)
		{
			if (speed >= moveAnim.minSpeed && speed <= moveAnim.maxSpeed)
			{
				currentMoveAnim = moveAnim.moveAnim;
				break;
			}
		}
	}

	public void Pause()
	{
		if (!isPause)
		{
			anims.Clear();
			IEnumerator enumerator = anim.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.Current;
				if (anim.IsPlaying(animationState.name))
				{
					anims.Add(animationState.name);
					anim[animationState.name].enabled = false;
				}
			}
		}
		isPause = true;
	}

	public void Restore()
	{
		if (isPause)
		{
			foreach (string anim in anims)
			{
				this.anim[anim].enabled = true;
			}
		}
		isPause = false;
	}

	public void DecelerateMoveSpeed(float rate)
	{
		foreach (MoveAnimationData moveAnim in moveAnims)
		{
			anim[moveAnim.moveAnim.name].speed -= rate;
		}
	}

	public void RestoreMoveSpeed(float rate)
	{
		foreach (MoveAnimationData moveAnim in moveAnims)
		{
			anim[moveAnim.moveAnim.name].speed += rate;
		}
	}

	public void OnStand()
	{
		if (!isPause)
		{
			anim.CrossFade(standAnim.name);
		}
	}

	public void OnAttack()
	{
		if (!isPause && !OnAttackPreparation())
		{
			OnRealAttack();
			StopWaitting();
		}
	}

	public void OnHurt()
	{
		if (!isPause)
		{
			string text = hurtAnims[UnityEngine.Random.Range(0, hurtAnims.Count)].name;
			if (anim.IsPlaying(text))
			{
				anim.Stop(text);
			}
			anim[text].wrapMode = WrapMode.ClampForever;
			anim.Play(text);
		}
	}

	public void OnLaserHurt()
	{
		if (isPause)
		{
			return;
		}
		if (currentLasrHurtAnim == null && laserHurtAnims != null && laserHurtAnims.Count > 0)
		{
			currentLasrHurtAnim = laserHurtAnims[UnityEngine.Random.Range(0, laserHurtAnims.Count)].name;
		}
		if (currentLasrHurtAnim != null)
		{
			if (anim.IsPlaying(currentLasrHurtAnim))
			{
				anim.Stop(currentLasrHurtAnim);
			}
			anim[currentLasrHurtAnim].wrapMode = WrapMode.ClampForever;
			anim.Play(currentLasrHurtAnim);
		}
		else
		{
			OnHurt();
		}
	}

	public void OnMeleeHurt()
	{
		if (isPause)
		{
			return;
		}
		if (null == meleeHurtAnim)
		{
			OnHurt();
			return;
		}
		if (anim.IsPlaying(meleeHurtAnim.name))
		{
			anim.Stop(meleeHurtAnim.name);
		}
		anim[meleeHurtAnim.name].wrapMode = WrapMode.ClampForever;
		anim.Play(meleeHurtAnim.name);
	}

	public void OnAppear(int appearType)
	{
		_003COnAppear_003Ec__AnonStorey2D _003COnAppear_003Ec__AnonStorey2D = new _003COnAppear_003Ec__AnonStorey2D();
		_003COnAppear_003Ec__AnonStorey2D.appearType = appearType;
		if (!isPause)
		{
			List<AppearAnimationData> list = appearAnims.FindAll(_003COnAppear_003Ec__AnonStorey2D._003C_003Em__51);
			if (list != null && list.Count > 0)
			{
				string text = list[UnityEngine.Random.Range(0, list.Count)].appearAnim.name;
				anim.Play(text);
			}
		}
	}

	protected bool OnAttackPreparation()
	{
		if (!isPause && waittingTime > 0f && null != attackPreparationAnim)
		{
			anim.Stop(attackPreparationAnim.name);
			anim[attackPreparationAnim.name].wrapMode = WrapMode.Loop;
			anim.CrossFade(attackPreparationAnim.name);
			BeginWaitting();
			return true;
		}
		return false;
	}

	protected void BeginWaitting()
	{
		waittingTimer = 0f;
		isWaitting = true;
	}

	protected void StopWaitting()
	{
		waittingTimer = 0f;
		isWaitting = false;
	}

	protected virtual void Update()
	{
		if (!isPause && isWaitting)
		{
			waittingTimer += Time.deltaTime;
			if (waittingTimer >= waittingTime)
			{
				OnRealAttack();
				StopWaitting();
			}
		}
	}

	protected void OnRealAttack()
	{
		if (!isPause)
		{
			anim.Stop(attackAnim.name);
			anim[attackAnim.name].wrapMode = WrapMode.ClampForever;
			anim.CrossFade(attackAnim.name);
		}
	}

	private void OnHurtAnimOver()
	{
		if (null != notifyObj && !string.IsNullOrEmpty(function))
		{
			notifyObj.SendMessage(function);
		}
		currentLasrHurtAnim = null;
	}
}
