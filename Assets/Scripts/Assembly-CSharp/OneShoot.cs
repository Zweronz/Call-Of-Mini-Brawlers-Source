using UnityEngine;

public class OneShoot : MonoBehaviour
{
	private float time;

	private float timer;

	private void Start()
	{
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren == null)
		{
			return;
		}
		ParticleSystem[] array = componentsInChildren;
		foreach (ParticleSystem particleSystem in array)
		{
			if (time < particleSystem.startLifetime)
			{
				time = particleSystem.startLifetime;
			}
		}
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > time)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
