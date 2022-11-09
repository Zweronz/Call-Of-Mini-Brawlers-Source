using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

public class GameLoading : MonoBehaviour
{
	public static string loadingScene;

	public static Func<bool> action;

	public TUILabel tips;

	public List<string> tipsIDs;

	private AsyncOperation async;

	private bool needWaitting;

	private void Awake()
	{
		EventCenter.Instance.Register<GameLoadingWaittingEndEvent>(HandleGameLoadingWaittingEndEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<GameLoadingWaittingEndEvent>(HandleGameLoadingWaittingEndEvent);
	}

	private void Start()
	{
		if (tipsIDs != null && tipsIDs.Count > 0)
		{
			tips.TextID = tipsIDs[UnityEngine.Random.Range(0, tipsIDs.Count)];
		}
		GameObject gameObject = GameObject.Find("Mus_map(Clone)");
		if (null != gameObject)
		{
			UnityEngine.Object.Destroy(gameObject);
			ZS_UIAudioManager.createMusic = true;
		}
		if (action != null)
		{
			needWaitting = action();
		}
		if (!needWaitting)
		{
			StartCoroutine(LoadScene());
		}
	}

	public void HandleGameLoadingWaittingEndEvent(object sender, GameLoadingWaittingEndEvent evt)
	{
		if (evt.ClearAction)
		{
			action = null;
		}
		needWaitting = false;
		StartCoroutine(LoadScene());
	}

	private IEnumerator LoadScene()
	{
		async = Application.LoadLevelAsync(loadingScene);
		Player.Instance.Save();
		yield return async;
	}
}
