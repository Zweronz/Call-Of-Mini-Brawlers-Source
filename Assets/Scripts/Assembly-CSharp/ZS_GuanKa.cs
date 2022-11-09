using System;
using System.Collections.Generic;
using Event;
using UnityEngine;

public class ZS_GuanKa : MonoBehaviour
{
	public TUIMeshSprite yStar;

	public TUIMeshSprite bStar;

	public Transform starParent;

	public float starWidth;

	public float starPosY;

	public float starPosZ;

	private Vector3 pos;

	private List<ZS_MapBoxInfo> mapBoxList;

	private Func<string, bool> startGameDelegate;

	private void SetStartGameHandle(Func<string, bool> handle)
	{
		startGameDelegate = handle;
	}

	private void ClearStartGameHandle()
	{
		startGameDelegate = null;
	}

	private bool StartGame(string secneName)
	{
		Application.LoadLevel(secneName);
		return true;
	}

	private void GetAllMapBoxInfo(List<ZS_MapBoxInfo> list)
	{
		mapBoxList = list;
	}

	private void Start()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishAllMapBoxInfo(GetAllMapBoxInfo));
		SetStartGameHandle(StartGame);
		SetDifficultyStar(4, 1);
	}

	private void SetDifficultyStar(int yStarCount, int bStarCount)
	{
		if (null != yStar && yStarCount > 0)
		{
			for (int i = 0; i < yStarCount; i++)
			{
				TUIMeshSprite tUIMeshSprite = UnityEngine.Object.Instantiate(yStar) as TUIMeshSprite;
				tUIMeshSprite.transform.parent = starParent;
				pos.x = starWidth * (float)i;
				pos.y = starPosY;
				pos.z = starPosZ;
				tUIMeshSprite.transform.localPosition = pos;
			}
		}
		if (null != bStar && bStarCount > 0)
		{
			for (int j = 0; j < bStarCount; j++)
			{
				TUIMeshSprite tUIMeshSprite2 = UnityEngine.Object.Instantiate(bStar) as TUIMeshSprite;
				tUIMeshSprite2.transform.parent = starParent;
				pos.x = (float)yStarCount * starWidth + starWidth * (float)j;
				pos.y = starPosY;
				pos.z = starPosZ;
				tUIMeshSprite2.transform.localPosition = pos;
			}
		}
	}

	private void StartGameEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_start);
			startGameDelegate("MissionScene!");
		}
	}

	private void HideGuanKaBox(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
			UnityEngine.Object.Destroy(base.gameObject, 0.2f);
		}
	}
}
