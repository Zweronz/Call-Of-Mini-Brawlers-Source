using UnityEngine;

public class ZS_EquipShowPrice : MonoBehaviour
{
	public TUILabel price;

	public TUIMeshSprite mSprite;

	public void ShowPriceObject(ZS_Money money)
	{
		if (money.Gold > 0.0)
		{
			mSprite.texture = "jinbi";
			price.Text = ZS_TUIMisc.FormatToString(money.Gold);
		}
		else if (money.Tcystal > 0.0)
		{
			mSprite.texture = "shuijing";
			price.Text = ZS_TUIMisc.FormatToString(money.Tcystal);
		}
	}

	public void HidePriceObject()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
