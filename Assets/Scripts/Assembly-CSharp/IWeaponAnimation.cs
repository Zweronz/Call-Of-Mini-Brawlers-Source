public interface IWeaponAnimation
{
	void BeEnable();

	void BeDisable();

	void PlayStandAnimation(float fadeLength = 0.3f);

	void PlayMoveAnimation();

	void PlayAttackAnimation();

	void PlayDeadAnimation();

	void PlayReviveAnimation();

	void OnChangeMoveSpeed(float speed);

	void ImproveAttackAnimationSpeed(float attackSpeed);
}
