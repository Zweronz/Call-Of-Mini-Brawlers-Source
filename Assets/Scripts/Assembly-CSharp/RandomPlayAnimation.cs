using System.Collections.Generic;
using UnityEngine;

public class RandomPlayAnimation : MonoBehaviour
{
	public Animation anim;

	public List<AnimationClip> clips;

	private void Start()
	{
		if (clips != null && clips.Count > 0)
		{
			AnimationClip animationClip = clips[ZombieStreetCommon.Random(0, clips.Count)];
			if (null != animationClip)
			{
				anim.Play(animationClip.name);
			}
		}
	}
}
