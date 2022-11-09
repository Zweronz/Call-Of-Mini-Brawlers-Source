using UnityEngine;

public class KeyboardControlModel : MonoBehaviour
{
	public KeyCode forward;

	public KeyCode backward;

	public KeyCode meleeAttack;

	public KeyCode shoot;

	public KeyCode switchWeapon;

	public KeyCode avoid;

	private void LateUpdate()
	{
		if (Input.GetKeyDown(shoot))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Down);
		}
		if (Input.GetKeyDown(meleeAttack))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Down);
		}
		if (Input.GetKeyDown(forward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Down);
		}
		if (Input.GetKeyDown(backward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Down);
		}
		if (Input.GetKey(forward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Hold);
		}
		if (Input.GetKey(backward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Hold);
		}
		if (Input.GetKey(meleeAttack))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Hold);
		}
		if (Input.GetKey(shoot))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Hold);
		}
		if (Input.GetKeyUp(forward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Forward, CharacterInputJudgment.InputType.Up);
		}
		if (Input.GetKeyUp(backward))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Backward, CharacterInputJudgment.InputType.Up);
		}
		if (Input.GetKeyUp(meleeAttack))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.MeleeAttack, CharacterInputJudgment.InputType.Up);
		}
		if (Input.GetKeyUp(shoot))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Shoot, CharacterInputJudgment.InputType.Up);
		}
		if (Input.GetKeyUp(switchWeapon))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Switch, CharacterInputJudgment.InputType.Up);
		}
		if (Input.GetKeyUp(avoid))
		{
			CharacterInputJudgment.Instance.HandleInputEvent(CharacterInputJudgment.ControlType.Avoid, CharacterInputJudgment.InputType.Up);
		}
	}
}
