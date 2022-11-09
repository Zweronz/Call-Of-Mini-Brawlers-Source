using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
	public AnimationClip clip;

	public WrapMode wrapMode;

	public int layer;

	public int weight;

	public List<string> mixTransforms;
}
