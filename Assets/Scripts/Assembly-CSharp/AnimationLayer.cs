using System;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationLayer : MonoBehaviour
{
	[Serializable]
	public class AnimationLayerData
	{
		public AnimationClip clip;

		public int layer;

		public Transform[] mixTransforms;
	}

	[SerializeField]
	protected AnimationLayerData[] layerDatas;

	private void Awake()
	{
		if (layerDatas == null)
		{
			return;
		}
		AnimationLayerData[] array = layerDatas;
		foreach (AnimationLayerData animationLayerData in array)
		{
			if (!(null != animationLayerData.clip))
			{
				continue;
			}
			AnimationState animationState = base.GetComponent<Animation>()[animationLayerData.clip.name];
			animationState.layer = animationLayerData.layer;
			if (animationLayerData.mixTransforms == null)
			{
				continue;
			}
			Transform[] mixTransforms = animationLayerData.mixTransforms;
			foreach (Transform transform in mixTransforms)
			{
				if (null != transform)
				{
					animationState.AddMixingTransform(transform);
				}
			}
		}
	}
}
