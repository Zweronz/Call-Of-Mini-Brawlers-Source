using UnityEngine;

public abstract class WeaponInputJudgment : MonoBehaviour, IWeaponInputJudgment
{
	protected bool isLocked;

	public abstract bool Judge(CharacterInputJudgment.InputType inputType);

	public virtual void Lock()
	{
		isLocked = true;
	}

	public virtual void Unlock()
	{
		isLocked = false;
	}

	public virtual void Reset()
	{
	}
}
