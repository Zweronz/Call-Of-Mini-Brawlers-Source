using Event;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	public float hp = 5f;

	public ZombieAIModel aiModel;

	public ZombiePhysicsModel physicsModel;

	public ZombieAnimationModel animationModel;

	public ZombieEffectModel effectModel;

	public ZombieAttackModel attackModel;

	private float coefficientOfDamage = 1f;

	private EnemyBaseData baseData;

	private EnemyBaseHpDmgData baseDataEx;

	private ArenaMissionData.EnemyRate enemyRate;

	private bool isAttacking;

	private bool isDecelerating;

	private ZombieStreetTimer.TimerData timerData;

	private int hurtFendCount = 2;

	private bool isFrozen;

	private float stiff;

	private float stiffBase;

	private float deceleration;

	private float timeOfRestoration;

	public EnemyBaseData Data
	{
		get
		{
			return baseData;
		}
	}

	public EnemyBaseHpDmgData BaseData
	{
		get
		{
			return baseDataEx;
		}
	}

	public ArenaMissionData.EnemyRate EnemyRate
	{
		get
		{
			return enemyRate;
		}
	}

	public float AttackRange { get; private set; }

	public float CoefficientOfDamage
	{
		get
		{
			return coefficientOfDamage;
		}
	}

	public void Initialize(EnemyBaseData baseData, EnemyBaseHpDmgData baseDataEx)
	{
		this.baseData = baseData;
		this.baseDataEx = baseDataEx;
		AttackRange = baseData.attackRange;
		hp = baseData.coefficientOfHp * baseDataEx.hp;
		aiModel.alertRange = 100f;
		aiModel.meleeAttackRange = AttackRange;
		physicsModel.Speed = baseData.speed;
		physicsModel.frictionA = baseData.frictionA;
		animationModel.SwitchMoveAnim(physicsModel.Speed);
		animationModel.waittingTime = baseData.attackPreparationTime;
		stiff = baseData.stiff;
		stiffBase = stiff;
		deceleration = baseData.deceleration;
		timeOfRestoration = baseData.timeOfRestoration;
	}

	public void Initialize(EnemyBaseData baseData, EnemyBaseHpDmgData baseDataEx, ArenaMissionData.EnemyRate enemyRate)
	{
		this.baseData = baseData;
		this.baseDataEx = baseDataEx;
		this.enemyRate = enemyRate;
		AttackRange = baseData.attackRange * enemyRate.attackRangeRate;
		hp = baseData.coefficientOfHp * baseDataEx.hp * enemyRate.hpRate;
		aiModel.alertRange = 100f;
		aiModel.meleeAttackRange = AttackRange;
		physicsModel.Speed = baseData.speed * enemyRate.speedRate;
		physicsModel.frictionA = baseData.frictionA * enemyRate.frictionARate;
		animationModel.SwitchMoveAnim(physicsModel.Speed);
		animationModel.waittingTime = baseData.attackPreparationTime * enemyRate.attackPreparationTimeRate;
		stiff = baseData.stiff * enemyRate.stiffRate;
		stiffBase = stiff;
		deceleration = baseData.deceleration * enemyRate.decelerationRate;
		timeOfRestoration = baseData.timeOfRestoration * enemyRate.timeOfRestorationRate;
		coefficientOfDamage = enemyRate.damageRate;
	}

	public void Appear(Transform point)
	{
		int num = Data.appearType[ZombieStreetCommon.Random(0, Data.appearType.Count)];
		if (num == 2)
		{
			aiModel.locked = true;
			base.transform.position = point.position + point.forward * Random.Range(1.5f, 3.5f);
		}
		else
		{
			base.transform.position = point.position;
		}
		base.transform.rotation = point.rotation;
		animationModel.OnAppear(num);
		effectModel.OnAppear(num);
	}

	public void Appear(Vector3 position, Quaternion rotation)
	{
		int num = Data.appearType[ZombieStreetCommon.Random(0, Data.appearType.Count)];
		base.transform.position = position;
		base.transform.rotation = rotation;
		if (num == 2)
		{
			aiModel.locked = true;
		}
		animationModel.OnAppear(num);
		effectModel.OnAppear(num);
	}

	public void Disappear()
	{
		Object.DestroyImmediate(base.gameObject);
	}

	public void OnHurt(Gun gun, float damage)
	{
		effectModel.CloseLaserHurt();
		if (!isFrozen)
		{
			stiff -= gun.Data.stiff;
			if (stiff > 0f)
			{
				Decelerate();
			}
			else
			{
				hurtFendCount = 2;
				aiModel.locked = true;
				physicsModel.OnFend(gun.Data.fend, OnFendOver);
				animationModel.OnHurt();
				isAttacking = false;
				stiff = stiffBase;
				if (timerData != null)
				{
					Restore();
					ZombieStreetTimer.RemoveTimer(timerData);
					timerData = null;
				}
			}
		}
		effectModel.OnHurt();
		if (SufferDamage(damage))
		{
			PublishDeadEvent(ZombieDeadEvent.WeaponType.Gun, gun.Data.id);
			if (isFrozen)
			{
				OnFrozenDead();
			}
			else
			{
				OnDead();
			}
		}
	}

	public void OnLaserHurt(Gun gun, float damage)
	{
		if (!isFrozen)
		{
			stiff -= gun.Data.stiff;
			if (stiff > 0f)
			{
				Decelerate();
			}
			else
			{
				hurtFendCount = 0;
				aiModel.locked = true;
				animationModel.OnLaserHurt();
				isAttacking = false;
				stiff = stiffBase;
				if (timerData != null)
				{
					Restore();
					ZombieStreetTimer.RemoveTimer(timerData);
					timerData = null;
				}
			}
			effectModel.OnLaserHurt();
		}
		if (SufferDamage(damage))
		{
			PublishDeadEvent(ZombieDeadEvent.WeaponType.Gun, gun.Data.id);
			if (isFrozen)
			{
				OnFrozenDead();
			}
			else
			{
				OnLaserDead();
			}
		}
	}

	public void OnFrozenHurt(Gun gun, float damage)
	{
		effectModel.CloseLaserHurt();
		isFrozen = true;
		effectModel.OnFrozen(gun.Data.attackRange, OnFrozenOver);
		animationModel.Pause();
		aiModel.frozenLock = true;
		if (SufferDamage(damage))
		{
			PublishDeadEvent(ZombieDeadEvent.WeaponType.Gun, gun.Data.id);
			if (isFrozen)
			{
				OnFrozenDead();
			}
		}
	}

	private void OnFrozenOver()
	{
		isFrozen = false;
		animationModel.Restore();
		aiModel.frozenLock = false;
	}

	public void OnHurt(MeleeWeapon meleeWeapon, float damage)
	{
		effectModel.CloseLaserHurt();
		if (!isFrozen)
		{
			stiff -= meleeWeapon.Data.stiff;
			if (stiff > 0f)
			{
				Decelerate();
			}
			else
			{
				hurtFendCount = 2;
				aiModel.locked = true;
				physicsModel.OnFend(meleeWeapon.Data.fend, OnFendOver);
				animationModel.OnMeleeHurt();
				isAttacking = false;
				stiff = stiffBase;
				if (timerData != null)
				{
					Restore();
					ZombieStreetTimer.RemoveTimer(timerData);
					timerData = null;
				}
			}
		}
		effectModel.OnHurt();
		if (!SufferDamage(damage))
		{
			return;
		}
		PublishDeadEvent(ZombieDeadEvent.WeaponType.MeleeWeapon, meleeWeapon.Data.id);
		if (isFrozen)
		{
			OnFrozenDead();
			return;
		}
		switch (meleeWeapon.Data.type)
		{
		case MeleeWeaponData.Type.Blunt:
			OnDead();
			break;
		case MeleeWeaponData.Type.Sharp:
			OnMeleeDead();
			break;
		}
	}

	public void OnHurt(IItem item, float damage)
	{
		effectModel.CloseLaserHurt();
		effectModel.OnHurt();
		if (SufferDamage(damage))
		{
			PublishDeadEvent(ZombieDeadEvent.WeaponType.Item, item.BaseData.id);
			if (isFrozen)
			{
				OnFrozenDead();
			}
			else
			{
				OnDead();
			}
		}
	}

	private void PublishDeadEvent(ZombieDeadEvent.WeaponType wepType, string wepId)
	{
		EventCenter.Instance.Publish(null, new ZombieDeadEvent(Data.id, wepId, wepType, enemyRate));
	}

	private void Decelerate()
	{
		if (!isDecelerating)
		{
			physicsModel.DecelerateMoveSpeed(deceleration);
			animationModel.DecelerateMoveSpeed(deceleration);
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = timeOfRestoration;
			timerData.handler = OnDecelerateMoveSpeedOver;
			ZombieStreetTimer.Instance.AddTimer(timerData);
			isDecelerating = true;
		}
		else if (timerData != null)
		{
			timerData.invokeTimes = 1;
			timerData.time = timeOfRestoration;
		}
	}

	private void Restore()
	{
		physicsModel.RestoreMoveSpeed(deceleration);
		animationModel.RestoreMoveSpeed(deceleration);
		isDecelerating = false;
	}

	private void OnMove()
	{
		if (!isAttacking)
		{
			physicsModel.OnMove();
			animationModel.OnMove();
		}
	}

	private void OnMeleeAttack()
	{
		if (!isAttacking)
		{
			physicsModel.OnStand();
			animationModel.OnAttack();
			attackModel.BeginAttack();
		}
		isAttacking = true;
	}

	private void OnFaceTo(Transform trans)
	{
		physicsModel.OnFaceTo(trans);
	}

	private void OnFendOver()
	{
		hurtFendCount--;
		if (0 >= hurtFendCount)
		{
			aiModel.locked = false;
			effectModel.CloseLaserHurt();
		}
	}

	private void OnDead()
	{
		effectModel.OnDead();
		Object.DestroyImmediate(base.gameObject);
	}

	private void OnMeleeDead()
	{
		effectModel.OnMeleeDead();
		Object.DestroyImmediate(base.gameObject);
	}

	private void OnLaserDead()
	{
		effectModel.OnLaserDead();
		Object.DestroyImmediate(base.gameObject);
	}

	private void OnFrozenDead()
	{
		effectModel.OnFrozenDead();
		Object.DestroyImmediate(base.gameObject);
	}

	private bool SufferDamage(float damage)
	{
		hp -= damage;
		return hp <= 0f;
	}

	protected void OnAttackOver()
	{
		isAttacking = false;
		attackModel.EndAttack();
	}

	protected void OnRockOver()
	{
		aiModel.locked = false;
	}

	private void OnDestroy()
	{
		if (timerData != null)
		{
			ZombieStreetTimer.RemoveTimer(timerData);
		}
	}

	private void OnDecelerateMoveSpeedOver(ZombieStreetTimer.TimerData data)
	{
		if (timerData == data)
		{
			Restore();
			ZombieStreetTimer.RemoveTimer(timerData);
			timerData = null;
		}
	}

	public void Lock()
	{
		aiModel.locked = true;
	}
}
