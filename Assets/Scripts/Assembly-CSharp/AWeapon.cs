using UnityEngine;

public abstract class AWeapon<T> : MonoBehaviour where T : WeaponData
{
	public int[] attackLayers;

	public string animationSubTitle;

	public T Data { get; protected set; }

	public bool CanAttack { get; protected set; }

	public int AttackLayerMask
	{
		get
		{
			int num = 0;
			if (attackLayers != null)
			{
				int[] array = attackLayers;
				foreach (int num2 in array)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}
	}

	public GameObject Owner { get; set; }

	public abstract IWeaponInputJudgment Judgment { get; }

	public void Initialize(T data)
	{
		Data = data;
		DoInitialize();
	}

	protected virtual void DoInitialize()
	{
	}

	public void Attack()
	{
		DoAttack();
	}

	public virtual void OnEquip()
	{
	}

	public virtual void OnRemove()
	{
	}

	protected abstract void DoAttack();

	public void ChangeDamage(float rate)
	{
		Data.damage *= 1f + rate;
	}

	public void SubDamage(float rate)
	{
		Data.damage /= 1f + rate;
	}
}
