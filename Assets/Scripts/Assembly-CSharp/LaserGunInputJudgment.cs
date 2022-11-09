using UnityEngine;

public class LaserGunInputJudgment : WeaponInputJudgment
{
	public override bool Judge(CharacterInputJudgment.InputType inputType)
	{
		if (isLocked)
		{
			return false;
		}
		switch (inputType)
		{
		case CharacterInputJudgment.InputType.Down:
			return true;
		case CharacterInputJudgment.InputType.Hold:
			return true;
		default:
			NotifyCancelShoot();
			return false;
		}
	}

	private void NotifyCancelShoot()
	{
		SendMessage("CancelShoot", SendMessageOptions.DontRequireReceiver);
	}

	private void NotifyBeginShoot()
	{
		SendMessage("BeginLaserShoot", SendMessageOptions.DontRequireReceiver);
	}

	public override void Reset()
	{
		base.Reset();
	}
}
