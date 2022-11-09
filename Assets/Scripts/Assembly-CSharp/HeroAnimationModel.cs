using UnityEngine;

public class HeroAnimationModel : MonoBehaviour
{
	public Animation anim;

	public BoneFinder finder;

	public float moveSpeed;

	[SerializeField]
	private string defalutWeaponAnimationName;

	private WeaponAnimation weaponAnimation;

	private WeaponAnimation defaultWeaponAnimation;

	public WeaponAnimation WeaponAnimation
	{
		get
		{
			if (null == weaponAnimation)
			{
				weaponAnimation = defaultWeaponAnimation;
				Enable(weaponAnimation);
			}
			if (null != weaponAnimation)
			{
				weaponAnimation.OnChangeMoveSpeed(moveSpeed);
			}
			return weaponAnimation;
		}
		set
		{
			if (null == value && WeaponAnimation != defaultWeaponAnimation)
			{
				Disable(weaponAnimation);
				weaponAnimation = defaultWeaponAnimation;
				Enable(weaponAnimation);
			}
			else if (value != weaponAnimation)
			{
				Disable(weaponAnimation);
				weaponAnimation = value;
				Enable(weaponAnimation);
			}
		}
	}

	private void Awake()
	{
		defaultWeaponAnimation = GetComponent(defalutWeaponAnimationName) as WeaponAnimation;
		WeaponAnimation = defaultWeaponAnimation;
	}

	private void Enable(WeaponAnimation weaponAnimation)
	{
		if (null != weaponAnimation)
		{
			weaponAnimation.Bind(anim, finder);
			weaponAnimation.BeEnable();
		}
	}

	private void Disable(WeaponAnimation weaponAnimation)
	{
		if (null != weaponAnimation)
		{
			weaponAnimation.BeDisable();
		}
	}
}
