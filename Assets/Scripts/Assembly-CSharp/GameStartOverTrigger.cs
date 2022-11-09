using Event;
using UnityEngine;

public class GameStartOverTrigger : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.SetActiveRecursively(false);
	}

	private void GameStartEnd()
	{
		base.gameObject.SetActiveRecursively(false);
		EventCenter.Instance.Publish(this, new GameStartEndEvent());
	}

	private void GameOverEnd()
	{
		base.gameObject.SetActiveRecursively(false);
		EventCenter.Instance.Publish(this, new GameOverEndEvent());
	}
}
