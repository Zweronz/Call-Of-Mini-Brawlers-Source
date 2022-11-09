using System.Collections.Generic;
using UnityEngine;

public class HeroFaceModel : MonoBehaviour
{
	public List<Renderer> renderers;

	public List<Texture> normalTextures;

	public List<Texture> angryTextures;

	public void BeNormal()
	{
		ChangeFace(normalTextures);
	}

	public void BeAngry()
	{
		ChangeFace(angryTextures);
	}

	private void ChangeFace(List<Texture> face)
	{
		for (int i = 0; i < renderers.Count && i < face.Count; i++)
		{
			if (null != renderers[i].material)
			{
				renderers[i].material.mainTexture = face[i];
			}
		}
	}
}
