using System.Collections.Generic;
using UnityEngine;

public class BulletCaseEmitter : MonoBehaviour
{
	public BulletCase bulletCase;

	public Transform emitPoint;

	[SerializeField]
	protected int maxbulletCases = 10;

	private List<BulletCase> bulletCases = new List<BulletCase>();

	private int currentIndex;

	public void Emit()
	{
		GetCurrentBulletCase().Fly(emitPoint);
	}

	public void Clear()
	{
		foreach (BulletCase bulletCase in bulletCases)
		{
			Object.DestroyImmediate(bulletCase.gameObject);
		}
		bulletCases.Clear();
		currentIndex = 0;
	}

	private BulletCase GetCurrentBulletCase()
	{
		if (bulletCases.Count < maxbulletCases)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(bulletCase.gameObject);
			bulletCases.Add(gameObject.GetComponent<BulletCase>());
			currentIndex = bulletCases.Count - 1;
		}
		else
		{
			currentIndex = ++currentIndex % maxbulletCases;
		}
		return bulletCases[currentIndex];
	}
}
