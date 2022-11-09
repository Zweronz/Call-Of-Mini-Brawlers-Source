using UnityEngine;

public class MoveInputJudgment : MonoBehaviour
{
	public enum Status
	{
		Forward = 0,
		Backward = 1,
		StandForward = 2,
		StandBackward = 3
	}

	public Status currentStatus = Status.StandForward;

	private int lockNumber;

	public bool IsLock
	{
		get
		{
			return lockNumber > 0;
		}
	}

	private void InputController_Forward(CharacterInputJudgment.InputType inputType)
	{
		if (IsLock)
		{
			return;
		}
		switch (inputType)
		{
		case CharacterInputJudgment.InputType.Down:
			if (currentStatus == Status.Backward || currentStatus == Status.StandBackward)
			{
				TurnAround();
			}
			Move();
			currentStatus = Status.Forward;
			break;
		case CharacterInputJudgment.InputType.Hold:
			if (currentStatus == Status.Forward)
			{
				Move();
				currentStatus = Status.Forward;
			}
			break;
		case CharacterInputJudgment.InputType.Up:
			if (currentStatus == Status.Forward)
			{
				Stand();
				currentStatus = Status.StandForward;
			}
			break;
		}
	}

	private void InputController_Backward(CharacterInputJudgment.InputType inputType)
	{
		if (IsLock)
		{
			return;
		}
		switch (inputType)
		{
		case CharacterInputJudgment.InputType.Down:
			if (currentStatus == Status.Forward || currentStatus == Status.StandForward)
			{
				TurnAround();
			}
			Move();
			currentStatus = Status.Backward;
			break;
		case CharacterInputJudgment.InputType.Hold:
			if (currentStatus == Status.Backward)
			{
				Move();
				currentStatus = Status.Backward;
			}
			break;
		case CharacterInputJudgment.InputType.Up:
			if (currentStatus == Status.Backward)
			{
				Stand();
				currentStatus = Status.StandBackward;
			}
			break;
		}
	}

	private void Move()
	{
		SendMessage("OnMove", SendMessageOptions.DontRequireReceiver);
	}

	private void Stand()
	{
		SendMessage("OnStand", SendMessageOptions.DontRequireReceiver);
	}

	private void TurnAround()
	{
		SendMessage("OnTurnAround", SendMessageOptions.DontRequireReceiver);
	}

	public void LockMove()
	{
		lockNumber++;
	}

	public void UnlockMove()
	{
		if (lockNumber > 0)
		{
			lockNumber--;
		}
	}
}
