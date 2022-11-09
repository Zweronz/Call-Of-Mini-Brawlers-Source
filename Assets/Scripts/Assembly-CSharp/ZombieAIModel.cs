using BehaviorTree;
using UnityEngine;

public class ZombieAIModel : MonoBehaviour
{
	public GameObject bindObj;

	public bool locked;

	public float alertRange = 4f;

	public float meleeAttackRange = 1f;

	public float disappearRange = 10f;

	private Behavior root;

	public bool frozenLock;

	private void Start()
	{
		root = new Selector().AddChild(new Sequence().AddChild(new ConditionHaveTarget(bindObj)).AddChild(new Selector().AddChild(new Sequence().AddChild(new ConditionTargetOutRange(bindObj, disappearRange)).AddChild(new ActionDisappear(bindObj))).AddChild(new Sequence().AddChild(new ActionFaceto(bindObj)).AddChild(new Selector().AddChild(new Sequence().AddChild(new ConditionTargetInRange(bindObj, meleeAttackRange)).AddChild(new ActionMeleeAttack(bindObj))).AddChild(new ActionMove(bindObj)))))).AddChild(new Sequence().AddChild(new ConditionEnemyCountInArea(bindObj, "Hero", alertRange, 1u, 10u)).AddChild(new ActionTarget(bindObj))).AddChild(new ActionIdle(bindObj));
	}

	private void Update()
	{
		if (!locked && !frozenLock)
		{
			root.Tick();
		}
	}
}
