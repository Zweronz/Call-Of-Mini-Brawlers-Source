using Fight;
using UnityEngine;

public class FightManagerMono : MonoBehaviour
{
	private void Update()
	{
		FightManager.Instance.Update();
	}
}
