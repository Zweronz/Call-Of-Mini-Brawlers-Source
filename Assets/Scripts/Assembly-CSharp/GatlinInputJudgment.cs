public class GatlinInputJudgment : AssaultRifleInputJudgment
{
	public delegate void OnInputTypeUp();

	public OnInputTypeUp del;

	public override bool Judge(CharacterInputJudgment.InputType inputType)
	{
		if (inputType == CharacterInputJudgment.InputType.Up && del != null)
		{
			del();
		}
		return base.Judge(inputType);
	}
}
