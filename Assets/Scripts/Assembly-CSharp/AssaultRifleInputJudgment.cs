public class AssaultRifleInputJudgment : WeaponInputJudgment
{
	public override bool Judge(CharacterInputJudgment.InputType inputType)
	{
		return !isLocked && CharacterInputJudgment.InputType.Up != inputType;
	}
}
