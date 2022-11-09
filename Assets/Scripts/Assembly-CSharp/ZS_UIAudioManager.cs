using UnityEngine;

public class ZS_UIAudioManager
{
	public static bool createMusic = true;

	public static void PlayAudio(SoundKind kind, bool loop = false)
	{
		GameObject gameObject = null;
		switch (kind)
		{
		case SoundKind.UI_bonus:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_bonus")) as GameObject;
			break;
		case SoundKind.UI_hero_use:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_hero_use")) as GameObject;
			break;
		case SoundKind.UI_hero_lock:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_hero_lock")) as GameObject;
			break;
		case SoundKind.UI_weapon_upgrad:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_weapon_upgrad")) as GameObject;
			break;
		case SoundKind.UI_weapon_equip:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_weapon_equip")) as GameObject;
			break;
		case SoundKind.UI_weapon_unwield:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_weapon_unwield")) as GameObject;
			break;
		case SoundKind.UI_topbutton_in:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_topbutton_in")) as GameObject;
			break;
		case SoundKind.UI_topbutton_out:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_topbutton_out")) as GameObject;
			break;
		case SoundKind.UI_moveon:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_moveon")) as GameObject;
			break;
		case SoundKind.UI_ok:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_ok")) as GameObject;
			break;
		case SoundKind.UI_no_back:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_no")) as GameObject;
			break;
		case SoundKind.UI_start:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_start")) as GameObject;
			break;
		case SoundKind.UI_popup:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_popup")) as GameObject;
			break;
		case SoundKind.UI_buy:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/UI_buy")) as GameObject;
			break;
		}
		if (null != gameObject)
		{
			TAudioEffectRandom component = gameObject.GetComponent<TAudioEffectRandom>();
			if (!loop)
			{
				component.Trigger();
				Object.DestroyObject(gameObject, component.audioClips[0].length * Time.timeScale);
			}
			else
			{
				component.Trigger();
				Object.DontDestroyOnLoad(gameObject);
			}
		}
	}

	public static void PlayMusic(SoundKind kind, bool loop = false)
	{
		GameObject gameObject = null;
		switch (kind)
		{
		case SoundKind.Mus_win:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/Mus_win")) as GameObject;
			break;
		case SoundKind.Mus_lose:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/Mus_lose")) as GameObject;
			break;
		case SoundKind.Mus_map:
			gameObject = Object.Instantiate(Resources.Load("SoundEvent/Mus_map")) as GameObject;
			break;
		}
		if (null != gameObject)
		{
			TAudioEffectRandom component = gameObject.GetComponent<TAudioEffectRandom>();
			if (!loop)
			{
				component.Trigger();
				Object.DestroyObject(gameObject, component.audioClips[0].length * Time.timeScale);
			}
			else if (createMusic)
			{
				component.loopMode = TAudioEffectRandom.LoopMode.MultiLoop;
				Object.DontDestroyOnLoad(gameObject);
				component.Trigger();
				createMusic = false;
			}
			else
			{
				Object.DestroyObject(gameObject);
			}
		}
	}
}
