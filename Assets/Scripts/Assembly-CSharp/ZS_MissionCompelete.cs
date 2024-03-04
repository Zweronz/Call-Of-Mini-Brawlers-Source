using System;
using System.Collections;
using Event;
using UnityEngine;

public class ZS_MissionCompelete : MonoBehaviour
{
	public TUILabel expLab;

	public TUILabel killLab;

	public TUILabel moneyLab;

	public TUILabel bonusLab;

	public float rollTime = 1f;

	public float flyTime = 1f;

	public float waitTime = 1.5f;

	public float waitSeconds = 0.5f;

	public TextAsset cn;

	public TextAsset en;

	private ZS_MissRewardInfo missInfo;

	public TUIFade fade;

	public TUILabel levLabel;

	public TUILabel goldLabel;

	public TUILabel tcyLabel;

	public TUILabel levPercentLab;

	public int frameCount = 50;

	public TUISlider slider;

	private void GetMissionRewardInfo(ZS_MissRewardInfo info)
	{
		missInfo = info;
	}

	private void Awake()
	{
		Time.timeScale = 1f;
		if (null != fade)
		{
			fade.FadeIn();
		}
		missInfo = new ZS_MissRewardInfo();
		missInfo.money = new ZS_Money(100.0, 0.0);
		missInfo.exp = 100;
		missInfo.killCount = 100;
		missInfo.bonus = 100;
		missInfo.isVictory = false;
		missInfo.levelUp = 2;
		missInfo.currentLevel = 2;
		missInfo.preExperience = 0.56f;
		missInfo.currentExperience = 0.998f;
		missInfo.avatarOwnMoney = new ZS_Money(10000000.0, 10000000.0);
		TUITextManager.Instance().Parser("lan/" + en.name, "lan/" + cn.name);
	}

	private void Start()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishMissRewardEvent(GetMissionRewardInfo));
		if (missInfo.isVictory)
		{
			ZS_UIAudioManager.PlayMusic(SoundKind.Mus_win);
		}
		else
		{
			ZS_UIAudioManager.PlayMusic(SoundKind.Mus_lose);
		}
		int gold = Convert.ToInt32(missInfo.avatarOwnMoney.Gold - missInfo.money.Gold);
		int tcystal = Convert.ToInt32(missInfo.avatarOwnMoney.Tcystal - missInfo.money.Tcystal);
		SetTopInfo(missInfo.currentLevel - missInfo.levelUp, gold, tcystal, missInfo.preExperience);
	}

	private IEnumerator RollCoroutines()
	{
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(SetLabInfomation(missInfo));
		StartCoroutine(ChangeAvtarExperience(expLab, missInfo, waitTime, flyTime));
		StartCoroutine(flyGold(moneyLab, goldLabel, waitTime, flyTime, missInfo.money.Gold, Convert.ToInt32(missInfo.avatarOwnMoney.Gold - missInfo.money.Gold)));
	}

	private IEnumerator ChangeAvtarExperience(TUILabel label, ZS_MissRewardInfo info, float waitSeconds, float flySeconds)
	{
		Vector3 startPos = label.transform.position;
		Vector3 endPos = levPercentLab.transform.position;
		float distance_x = endPos.x - startPos.x;
		float distance_y = endPos.y - startPos.y;
		float speed_x = distance_x / flySeconds;
		float speed_y = distance_y / flySeconds;
		float moveDis_x = 0f;
		float moveDis_y = 0f;
		yield return new WaitForSeconds(waitSeconds);
		GameObject temp = UnityEngine.Object.Instantiate(label.gameObject) as GameObject;
		while (true)
		{
			float time = Time.deltaTime;
			moveDis_x += speed_x * time;
			moveDis_y += speed_y * time;
			if (Mathf.Abs(moveDis_x) >= Mathf.Abs(distance_x) && Mathf.Abs(moveDis_y) >= Mathf.Abs(distance_y))
			{
				break;
			}
			temp.transform.position = new Vector3(startPos.x + moveDis_x, startPos.y + moveDis_y, -8f);
			yield return true;
		}
		UnityEngine.Object.Destroy(temp);
		StartCoroutine(ChangeAvtarExperienceImage(missInfo, GetRollTime(info)));
	}

	private IEnumerator ChangeAvtarExperienceImage(ZS_MissRewardInfo info, float seconds)
	{
		float prePercent = info.preExperience;
		float currentPercent = info.currentExperience;
		float time = Time.deltaTime;
		float speed = 0f;
		int count2 = 0;
		speed = ((info.levelUp <= 0) ? ((currentPercent - prePercent) / seconds) : ((currentPercent + (float)info.levelUp - prePercent) / seconds));
		while (true)
		{
			if (info.levelUp > 0)
			{
				if (count2 == 0)
				{
					prePercent += speed * time;
					if (prePercent >= 1f)
					{
						ZS_TUIMisc.SetLabel(levPercentLab, "0%");
						ZS_TUIMisc.SetLabel(levLabel, (Convert.ToInt32(levLabel.Text) + 1).ToString());
						slider.sliderValue = 1f;
						prePercent = 0f;
						count2++;
					}
					else
					{
						ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(prePercent * 100f) + "%");
						slider.sliderValue = prePercent;
					}
				}
				else if (count2 > 0 && count2 < info.levelUp)
				{
					prePercent += speed * time;
					if (prePercent >= 1f)
					{
						ZS_TUIMisc.SetLabel(levPercentLab, "0%");
						ZS_TUIMisc.SetLabel(levLabel, (Convert.ToInt32(levLabel.Text) + 1).ToString());
						slider.sliderValue = 1f;
						prePercent = 0f;
						count2++;
					}
					else
					{
						ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(prePercent * 100f) + "%");
						slider.sliderValue = prePercent;
					}
				}
				else if (count2 == info.levelUp)
				{
					prePercent += speed * time;
					if (prePercent >= info.currentExperience)
					{
						ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(currentPercent * 100f) + "%");
						slider.sliderValue = currentPercent;
						count2 = 0;
						yield break;
					}
					ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(prePercent * 100f) + "%");
					slider.sliderValue = prePercent;
				}
			}
			else
			{
				prePercent += speed * time;
				if (prePercent >= currentPercent)
				{
					break;
				}
				ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(prePercent * 100f) + "%");
				slider.sliderValue = prePercent;
			}
			yield return true;
		}
		ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(currentPercent * 100f) + "%");
		slider.sliderValue = currentPercent;
	}

	private IEnumerator SetLabInfomation(ZS_MissRewardInfo info)
	{
		StartCoroutine(ZS_TUIMisc.ChangeNumCoroutine(expLab, missInfo.exp, frameCount, 0.0));
		StartCoroutine(ZS_TUIMisc.ChangeNumCoroutine(killLab, missInfo.killCount, frameCount, 0.0));
		StartCoroutine(ZS_TUIMisc.ChangeNumCoroutine(moneyLab, missInfo.money.Gold, frameCount, 0.0));
		StartCoroutine(ZS_TUIMisc.ChangeNumCoroutine(bonusLab, missInfo.bonus, frameCount, 0.0));
		yield return true;
	}

	private IEnumerator flyGold(TUILabel startLab, TUILabel endLab, float waitSeconds, float flySeconds, double addValue, double startValue)
	{
		Vector3 startPos = startLab.transform.position;
		Vector3 endPos = endLab.transform.position;
		float distance_x = endPos.x - startPos.x;
		float distance_y = endPos.y - startPos.y;
		float speed_x = distance_x / flySeconds;
		float speed_y = distance_y / flySeconds;
		float moveDis_x = 0f;
		float moveDis_y = 0f;
		yield return new WaitForSeconds(waitSeconds);
		GameObject temp_ = UnityEngine.Object.Instantiate(startLab.gameObject) as GameObject;
		while (true)
		{
			float time = Time.deltaTime;
			moveDis_x += speed_x * time;
			moveDis_y += speed_y * time;
			if (Mathf.Abs(moveDis_x) >= Mathf.Abs(distance_x) && Mathf.Abs(moveDis_y) >= Mathf.Abs(distance_y))
			{
				break;
			}
			temp_.transform.position = new Vector3(startPos.x + moveDis_x, startPos.y + moveDis_y, -8f);
			yield return true;
		}
		UnityEngine.Object.Destroy(temp_);
		StartCoroutine(ZS_TUIMisc.ChangeNumCoroutine(endLab, addValue, 50, startValue));
	}

	private void SetTopInfo(int heroLevel, int gold, int tcystal, float experience)
	{
		slider.sliderValue = experience;
		ZS_TUIMisc.SetLabel(levLabel, heroLevel.ToString());
		ZS_TUIMisc.SetLabel(goldLabel, ZS_TUIMisc.FormatToString(gold));
		ZS_TUIMisc.SetLabel(tcyLabel, ZS_TUIMisc.FormatToString(tcystal));
		ZS_TUIMisc.SetLabel(levPercentLab, Mathf.Floor(experience * 100f) + "%");
	}

	private void DeterEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			if (null != fade)
			{
				fade.FadeOut(ZS_TUIMisc.indexScene);
			}
			else
			{
				Application.LoadLevel(ZS_TUIMisc.indexScene);
			}
		}
	}

	private float GetRollTime(ZS_MissRewardInfo info)
	{
		float num = 0f;
		num = ((info.levelUp <= 0) ? (info.currentExperience + 1f - info.preExperience) : (info.currentExperience + Convert.ToSingle(info.levelUp) - info.preExperience));
		return num / rollTime;
	}

	private void TriggerAnniuEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
		}
	}
}
