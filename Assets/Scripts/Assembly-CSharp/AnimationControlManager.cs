using UnityEngine;

public class AnimationControlManager : MonoBehaviour
{
	[SerializeField]
	private string defalutWeaponAnimationControlName;

	private IWeaponAnimation weaponAnimationControl;

	private IWeaponAnimation defaultWeaponAnimationControl;

	public IWeaponAnimation WeaponAnimationControl
	{
		get
		{
			if (weaponAnimationControl == null)
			{
				weaponAnimationControl = defaultWeaponAnimationControl;
				Enable(weaponAnimationControl);
			}
			return weaponAnimationControl;
		}
		set
		{
			if (value == null && WeaponAnimationControl != defaultWeaponAnimationControl)
			{
				Disable(weaponAnimationControl);
				weaponAnimationControl = defaultWeaponAnimationControl;
				Enable(weaponAnimationControl);
			}
			else
			{
				Disable(weaponAnimationControl);
				weaponAnimationControl = value;
				Enable(weaponAnimationControl);
			}
		}
	}

	private void Awake()
	{
		defaultWeaponAnimationControl = GetComponent(defalutWeaponAnimationControlName) as IWeaponAnimation;
		WeaponAnimationControl = defaultWeaponAnimationControl;
	}

	private void Enable(IWeaponAnimation weaponAnimationControl)
	{
		if (weaponAnimationControl != null)
		{
			weaponAnimationControl.BeEnable();
		}
	}

	private void Disable(IWeaponAnimation weaponAnimationControl)
	{
		if (weaponAnimationControl != null)
		{
			weaponAnimationControl.BeDisable();
		}
	}
}
