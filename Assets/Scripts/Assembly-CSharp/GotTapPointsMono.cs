using Event;
using UnityEngine;

public class GotTapPointsMono : MonoBehaviour
{
	private void GotTapPoints(int tapPoints)
	{
		if (tapPoints > 0)
		{
			Player.Instance.AddCrystal(tapPoints);
			Player.Instance.Save();
			EventCenter.Instance.Publish(this, new CrystalChangedEvent(Player.Instance.Crystal));
		}
	}
}
