public class PistolInputJudgment : WeaponInputJudgment
{
	public override bool Judge(CharacterInputJudgment.InputType inputType)
	{
		return !isLocked && CharacterInputJudgment.InputType.Down == inputType;
	}
}
