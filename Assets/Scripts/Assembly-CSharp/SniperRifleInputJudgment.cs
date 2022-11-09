using UnityEngine;

public class SniperRifleInputJudgment : WeaponInputJudgment
{
	public float waittingTime = 1f;

	private bool isWaitting;

	private float startTime;

	private bool notifyedWaitting;

	public override bool Judge(CharacterInputJudgment.InputType inputType)
	{
		if (isLocked)
		{
			return false;
		}
		if (!isWaitting && inputType == CharacterInputJudgment.InputType.Down)
		{
			isWaitting = true;
			startTime = Time.time;
			notifyedWaitting = false;
			return true;
		}
		if (isWaitting && inputType == CharacterInputJudgment.InputType.Up)
		{
			if (Time.time - startTime >= waittingTime)
			{
				NotifyRealShoot();
			}
			else
			{
				NotifyCancelShoot();
			}
			isWaitting = false;
		}
		return false;
	}

	private void Update()
	{
		if (!isLocked && isWaitting && !notifyedWaitting && Time.time - startTime >= waittingTime)
		{
			NotifyWaittingOver();
			notifyedWaitting = true;
		}
	}

	private void NotifyRealShoot()
	{
		SendMessage("RealShoot", SendMessageOptions.DontRequireReceiver);
	}

	private void NotifyCancelShoot()
	{
		SendMessage("CancelShoot", SendMessageOptions.DontRequireReceiver);
	}

	private void NotifyWaittingOver()
	{
		SendMessage("WaittingOver", SendMessageOptions.DontRequireReceiver);
	}

	public override void Reset()
	{
		base.Reset();
		isWaitting = false;
		startTime = 0f;
		NotifyCancelShoot();
	}
}
