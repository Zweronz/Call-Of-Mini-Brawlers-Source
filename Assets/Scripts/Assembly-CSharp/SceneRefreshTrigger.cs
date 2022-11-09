using UnityEngine;

public class SceneRefreshTrigger : MonoBehaviour
{
	public enum Direction
	{
		Forward = 0,
		Backward = 1
	}

	public GameObject currentScene;

	public Direction direction;

	public WorldCreator worldCreator;

	private void Start()
	{
		worldCreator = GameObject.FindWithTag("WorldCreator").GetComponent<WorldCreator>();
	}

	private void Update()
	{
	}

	private void OnTriggerEnter()
	{
		switch (direction)
		{
		case Direction.Forward:
			worldCreator.MoveSceneForward(currentScene);
			break;
		case Direction.Backward:
			worldCreator.MoveSceneBackward(currentScene);
			break;
		}
	}
}
