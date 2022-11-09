using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLayer2 : MonoBehaviour
{
	[Serializable]
	public class AnimationLayerData
	{
		public AnimationClip clip;

		public int layer;

		public List<string> mixTransforms;
	}

	public Animation anim;

	public BoneFinder boneFinder;

	[SerializeField]
	protected List<AnimationLayerData> layerDatas;

	private void Awake()
	{
		boneFinder.Init();
		if (layerDatas == null)
		{
			return;
		}
		foreach (AnimationLayerData layerData in layerDatas)
		{
			if (!(null != layerData.clip))
			{
				continue;
			}
			AnimationState animationState = anim[layerData.clip.name];
			animationState.layer = layerData.layer;
			if (layerData.mixTransforms == null)
			{
				continue;
			}
			foreach (string mixTransform in layerData.mixTransforms)
			{
				if (mixTransform != null)
				{
					animationState.AddMixingTransform(boneFinder.Query(mixTransform).bone);
				}
			}
		}
	}
}
