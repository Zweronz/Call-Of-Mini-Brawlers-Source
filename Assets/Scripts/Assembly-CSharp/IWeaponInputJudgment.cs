public interface IWeaponInputJudgment
{
	bool Judge(CharacterInputJudgment.InputType inputType);

	void Reset();

	void Lock();

	void Unlock();
}
