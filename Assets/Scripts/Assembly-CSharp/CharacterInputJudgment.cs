using UnityEngine;

public class CharacterInputJudgment
{
	public enum InputType
	{
		Down = 0,
		Hold = 1,
		Up = 2
	}

	public enum ControlType
	{
		Forward = 0,
		Backward = 1,
		MeleeAttack = 2,
		Shoot = 3,
		Switch = 4,
		Avoid = 5
	}

	public GameObject character;

	private int isLocked;

	private static CharacterInputJudgment instance;

	private bool IsLocked
	{
		get
		{
			return isLocked > 0;
		}
	}

	public static CharacterInputJudgment Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new CharacterInputJudgment();
			}
			return instance;
		}
	}

	public void HandleInputEvent(ControlType controlType, InputType inputType)
	{
		if (!IsLocked && null != character)
		{
			character.SendMessage("InputController_" + controlType, inputType, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void Lock()
	{
		isLocked++;
	}

	public void Unlock()
	{
		if (isLocked > 0)
		{
			isLocked--;
		}
	}

	public void ClearLock()
	{
		isLocked = 0;
	}
}
