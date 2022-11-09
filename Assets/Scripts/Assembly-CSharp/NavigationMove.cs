using UnityEngine;

public class NavigationMove : MonoBehaviour
{
	public GameObject gamecenter;

	public GameObject tbank;

	private void Awake()
	{
		gamecenter.SetActiveRecursively(false);
		tbank.transform.position = gamecenter.transform.position;
	}
}
