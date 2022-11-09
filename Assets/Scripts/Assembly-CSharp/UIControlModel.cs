using UnityEngine;

public class UIControlModel : MonoBehaviour
{
	private bool simulateHoldForward;

	private bool simulateHoldBackward;

	private bool simulateHoldShoot;

	private bool simulateHoldMeleeAttack;

	private void HandleForward(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Down);
			simulateHoldForward = true;
		}
		else
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Up);
			simulateHoldForward = false;
		}
	}

	private void HandleBackward(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Down);
			simulateHoldBackward = true;
		}
		else
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Up);
			simulateHoldBackward = false;
		}
	}

	private void HandleMove(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1 || eventType == 3)
		{
			if (wparam == 0f)
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Down);
				simulateHoldForward = true;
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Up);
				simulateHoldBackward = false;
			}
			else
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Down);
				simulateHoldBackward = true;
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Up);
				simulateHoldForward = false;
			}
		}
		else if (wparam == 0f)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Up);
			simulateHoldForward = false;
		}
		else
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Up);
			simulateHoldBackward = false;
		}
	}

	private void HandleShoot(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Down);
			simulateHoldShoot = true;
		}
		else
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Up);
			simulateHoldShoot = false;
		}
	}

	private void HandleMeleeAttack(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 1)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Down);
			simulateHoldMeleeAttack = true;
		}
		else
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Up);
			simulateHoldMeleeAttack = false;
		}
	}

	private void HandleSwitchWeapon(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Switch, CharacterInputJudgment.InputType.Up);
		}
	}

	private void HandleAvoid(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Avoid, CharacterInputJudgment.InputType.Up);
		}
	}

	private void SimulateHold(CharacterInputJudgment.ControlType controlType)
	{
		CharacterInputJudgment.Instance.HandleInputEvent(controlType, CharacterInputJudgment.InputType.Hold);
	}

	private void LateUpdate()
	{
		if (simulateHoldForward)
		{
			SimulateHold(CharacterInputJudgment.ControlType.Forward);
		}
		if (simulateHoldBackward)
		{
			SimulateHold(CharacterInputJudgment.ControlType.Backward);
		}
		if (simulateHoldShoot)
		{
			SimulateHold(CharacterInputJudgment.ControlType.Shoot);
		}
		if (simulateHoldMeleeAttack)
		{
			SimulateHold(CharacterInputJudgment.ControlType.MeleeAttack);
		}
	}
}
