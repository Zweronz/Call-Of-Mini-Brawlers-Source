using UnityEngine;

public class ZS_AvatarUseGunAnimation : MonoBehaviour
{
	public ZS_EquipPanelMove movePanel;

	public void PlayAnim()
	{
		if (null != movePanel)
		{
			movePanel.PlayAnimation();
		}
	}

	public void PlayAnimalStart()
	{
		if (null != movePanel)
		{
			movePanel.PlayAnimalStart();
		}
	}
}
