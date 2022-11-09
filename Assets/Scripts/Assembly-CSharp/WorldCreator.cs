using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
	public Vector3 arsenalPosition;

	public Vector3 firstScenePosition;

	public Transform heroPoint;

	public Transform EndPoint;

	public GameObject scenePrefab;

	public float sceneLength;

	public float maxSceneCount = 2f;

	public List<Transform> refreshPoints;

	private LinkedList<GameObject> scenes = new LinkedList<GameObject>();

	public void CreateScene()
	{
		for (int i = 0; (float)i < maxSceneCount; i++)
		{
			GameObject gameObject = Object.Instantiate(scenePrefab) as GameObject;
			gameObject.name = scenePrefab.name + i;
			gameObject.transform.position = firstScenePosition + base.transform.forward * i * sceneLength;
			scenes.AddLast(gameObject);
		}
	}

	public void MoveSceneForward(GameObject current)
	{
		GetNextScene(current).transform.position = current.transform.position + base.transform.forward * sceneLength;
	}

	public void MoveSceneBackward(GameObject current)
	{
		GetPreviousScene(current).transform.position = current.transform.position - base.transform.forward * sceneLength;
	}

	public WeaponArsenal CreateArsenal()
	{
		GameObject gameObject = new GameObject("Arsenal");
		gameObject.tag = "Arsenal";
		gameObject.transform.position = arsenalPosition;
		return gameObject.AddComponent<WeaponArsenal>();
	}

	public void SetEndPoint(float length)
	{
		EndPoint.gameObject.SetActiveRecursively(0f != length);
		EndPoint.position = heroPoint.position + heroPoint.forward * (length + 4f);
	}

	private GameObject GetNextScene(GameObject current)
	{
		LinkedListNode<GameObject> linkedListNode = scenes.Find(current);
		return (linkedListNode != scenes.Last) ? linkedListNode.Next.Value : scenes.First.Value;
	}

	private GameObject GetPreviousScene(GameObject current)
	{
		LinkedListNode<GameObject> linkedListNode = scenes.Find(current);
		return (linkedListNode != scenes.First) ? linkedListNode.Previous.Value : scenes.Last.Value;
	}
}
