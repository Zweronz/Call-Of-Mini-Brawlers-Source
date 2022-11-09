using System.Collections;
using UnityEngine;

public class GameStartOver : MonoBehaviour
{
	public TUILabel label;

	public Animation anim;

	public AnimationClip startAnimClip;

	public AnimationClip overAnimClip;

	public void GameStart(string text)
	{
		StartCoroutine(DoStart(text));
	}

	private IEnumerator DoStart(string text)
	{
		yield return new WaitForSeconds(0.5f);
		label.gameObject.SetActiveRecursively(true);
		label.Text = text;
		TUIActiveAnimation an = TUIActiveAnimation.Play(anim, startAnimClip.name, TUIDirection.Forward);
		an.callWhenFinished = "GameStartEnd";
	}

	public void GameOver(string text)
	{
		StartCoroutine(DoGameOver(text));
	}

	private IEnumerator DoGameOver(string text)
	{
		yield return new WaitForEndOfFrame();
		label.gameObject.SetActiveRecursively(true);
		label.Text = text;
		TUIActiveAnimation an = TUIActiveAnimation.Play(anim, overAnimClip.name, TUIDirection.Forward);
		an.callWhenFinished = "GameOverEnd";
	}
}
