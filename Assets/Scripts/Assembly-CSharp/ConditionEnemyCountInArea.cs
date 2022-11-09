using BehaviorTree;
using UnityEngine;

public class ConditionEnemyCountInArea : Condition
{
	private GameObject obj;

	private string enemyTag;

	private float radius;

	private uint min;

	private uint max = uint.MaxValue;

	public ConditionEnemyCountInArea(GameObject obj, string enemyTag, float radius, uint min = 0u, uint max = uint.MaxValue)
	{
		this.obj = obj;
		this.enemyTag = enemyTag;
		this.radius = radius;
		this.min = min;
		this.max = max;
	}

	public override Status Update()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(enemyTag);
		if (array != null)
		{
			int num = 0;
			GameObject[] array2 = array;
			foreach (GameObject tag in array2)
			{
				if (Tool.InArea(obj, tag, radius))
				{
					num++;
				}
			}
			if (num >= min && num <= max)
			{
				return Status.Success;
			}
		}
		return Status.Failure;
	}
}
