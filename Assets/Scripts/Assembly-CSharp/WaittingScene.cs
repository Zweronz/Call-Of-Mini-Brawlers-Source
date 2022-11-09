using System.Collections;
using UnityEngine;

public class WaittingScene : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0.2f);
		Application.LoadLevel("FirstScene");
	}

	private void Update()
	{
	}
}
