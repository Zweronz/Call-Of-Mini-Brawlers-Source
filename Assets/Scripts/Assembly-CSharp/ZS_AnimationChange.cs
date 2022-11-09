using UnityEngine;

public class ZS_AnimationChange : MonoBehaviour
{
	[HideInInspector]
	public Animation[] subAnim;

	private int count;

	private void ChangeAnimationFade(string animName)
	{
		base.GetComponent<Animation>().CrossFade(animName);
	}

	private void ChangeAnimation(string animName)
	{
		base.GetComponent<Animation>().Play(animName);
	}

	private void Change_Animation(string animName)
	{
		base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		base.GetComponent<Animation>().Play(animName);
	}

	private void ChangeKninghtMeleeAnimation(string animName)
	{
		string[] array = animName.Split(',');
		if (array.Length == 2)
		{
			if (count < 3)
			{
				base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				base.GetComponent<Animation>().Play(array[1]);
				count++;
			}
			else
			{
				base.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
				base.GetComponent<Animation>().Play(array[0]);
				count = 0;
			}
		}
		else
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
			base.GetComponent<Animation>().Play(array[0]);
		}
	}

	private void ChangeMeleeAnimation(string animName)
	{
		base.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
		base.GetComponent<Animation>().Play(animName);
	}

	private void ChangeMeleeAnimationIdle(string animName)
	{
		base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		base.GetComponent<Animation>().Play(animName);
	}

	private void PlayLightSaderAnim()
	{
		if (subAnim != null)
		{
			Animation[] array = subAnim;
			foreach (Animation animation in array)
			{
				animation.wrapMode = WrapMode.ClampForever;
				animation.Play();
			}
		}
	}

	private void PlayEffectsSaderAnim()
	{
		if (subAnim == null)
		{
			return;
		}
		Animation[] array = subAnim;
		foreach (Animation animation in array)
		{
			if (animation.name.Equals("Light_saber@chuqiao"))
			{
				animation.Play();
			}
		}
	}
}
