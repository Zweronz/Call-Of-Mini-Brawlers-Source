using UnityEngine;

public class UIControlModel : MonoBehaviour
{
	private bool simulateHoldForward;

	private bool simulateHoldBackward;

	private bool simulateHoldShoot;

	private bool simulateHoldMeleeAttack;

	private KeyCode? currentMainKey;

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

		if (!Application.isMobilePlatform)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				HandleAvoid(null, 3, 0f, 0f, null);
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				HandleSwitchWeapon(null, 3, 0f, 0f, null);
			}

			if (Screen.lockCursor)
			{
				if (Input.GetMouseButton(0) && !simulateHoldMeleeAttack)
				{
					CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Down);
					simulateHoldShoot = true;
				}
				else if (simulateHoldShoot)
				{
					CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Up);
					simulateHoldShoot = false;
				}

			}

			if (Input.GetMouseButton(1) && !simulateHoldShoot)
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Down);
				simulateHoldMeleeAttack = true;
			}
			else if (simulateHoldMeleeAttack)
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Up);
				simulateHoldMeleeAttack = false;
			}


			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (Player.Instance.Items.Count > 0)
				{
					GameUIItem gameUIItem = FindObjectOfType<GameUIItem>();
					gameUIItem.HandleUseItem(gameUIItem.buttons[0], 3, 0f, 0f, null);
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (Player.Instance.Items.Count > 1)
				{
					GameUIItem gameUIItem = FindObjectOfType<GameUIItem>();
					gameUIItem.HandleUseItem(gameUIItem.buttons[1], 3, 0f, 0f, null);
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				if (Player.Instance.Items.Count > 2)
				{
					GameUIItem gameUIItem = FindObjectOfType<GameUIItem>();
					gameUIItem.HandleUseItem(gameUIItem.buttons[2], 3, 0f, 0f, null);
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				if (Player.Instance.Items.Count > 3)
				{
					GameUIItem gameUIItem = FindObjectOfType<GameUIItem>();
					gameUIItem.HandleUseItem(gameUIItem.buttons[3], 3, 0f, 0f, null);
				}
			}

			if (Input.GetKey(KeyCode.A) && (currentMainKey == KeyCode.D || !Input.GetKey(KeyCode.D)))
			{
				if (currentMainKey == null || !Input.GetKey(currentMainKey.Value))
				{
					currentMainKey = KeyCode.A;
				}

				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Down);
				simulateHoldBackward = true;
			}
			else if (simulateHoldBackward)
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Up);
				simulateHoldBackward = false;
			}

			if (Input.GetKey(KeyCode.D) && (currentMainKey == KeyCode.A || !Input.GetKey(KeyCode.A)))
			{
				if (currentMainKey == null || !Input.GetKey(currentMainKey.Value))
				{
					currentMainKey = KeyCode.D;
				}

				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Down);
				simulateHoldForward = true;
			}
			else if (simulateHoldForward)
			{
				CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Up);
				simulateHoldForward = false;
			}

			if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				currentMainKey = null;
			}
		}
	}
}
