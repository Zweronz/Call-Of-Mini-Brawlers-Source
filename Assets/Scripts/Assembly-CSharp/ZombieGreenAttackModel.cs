using Fight;
using UnityEngine;

public class ZombieGreenAttackModel : ZombieAttackModel
{
	public Zombie self;

	public Target target;

	public Animation anim;

	public AnimationClip attackClip;

	public int frame;

	public float range = 1f;

	private void Awake()
	{
		AnimationTriggerEvent animationTriggerEvent = new AnimationTriggerEvent();
		animationTriggerEvent.animationState = anim[attackClip.name];
		animationTriggerEvent.obj = base.gameObject;
		animationTriggerEvent.time = (float)frame / attackClip.frameRate;
		animationTriggerEvent.functionName = "TrueAttack";
		animationTriggerEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
		animationTriggerEvent.AddToClip();
	}

	public override void BeginAttack()
	{
	}

	private void TrueAttack()
	{
		if (Tool.InArea(self.gameObject, target.target, range))
		{
			FightManager.Instance.Add(new ZombieBiteFightBehavior(self.Data.id, target.target, self.CoefficientOfDamage));
		}
	}

	public override void EndAttack()
	{
	}
}
