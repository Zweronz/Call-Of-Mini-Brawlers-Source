using System.Collections.Generic;
using UnityEngine;

public class ZS_AnimaitonPlay : MonoBehaviour
{
	public Animation anim;

	public Transform weapPos;

	private string group;

	public void PlayAnimal()
	{
		Dictionary<string, string> anmationState = GetAnmationState();
		group = ZS_EquipUsingShow.usingEquipInfo[1].Group;
		if (anmationState.ContainsKey(group))
		{
			anim.wrapMode = WrapMode.ClampForever;
			anim.Play(anmationState[group]);
			anim[anmationState[group]].speed = 0f;
		}
	}

	public void PlayAnimalStart()
	{
		Dictionary<string, string> anmationState = GetAnmationState();
		group = ZS_EquipUsingShow.usingEquipInfo[1].Group;
		if (anmationState.ContainsKey(group))
		{
			anim[anmationState[group]].speed = 1f;
		}
	}

	public void PlayAnimal(string name)
	{
		Dictionary<string, string> anmationState = GetAnmationState();
		if (anmationState.ContainsKey(name))
		{
			anim.wrapMode = WrapMode.Loop;
			anim.Play(anmationState[name]);
		}
	}

	private Dictionary<string, string> GetAnmationState()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("Usp", "avatar_usp45@Idle02");
		dictionary.Add("DesertEagle", "avatar_usp45@Idle02");
		dictionary.Add("Mag", "avatar_usp45@Idle02");
		dictionary.Add("M1014", "avatar_XM1014@Idle02");
		dictionary.Add("870", "avatar_XM1014@Idle");
		dictionary.Add("Mp5", "avatar_MP5@Idle02");
		dictionary.Add("Uzi", "avatar_MP5@Idle02");
		dictionary.Add("KRISS", "avatar_kriss@Idle02");
		dictionary.Add("CryoGun", "avatar_MP5@Idle02");
		dictionary.Add("LaserGun", "avatar_kriss@Idle02");
		dictionary.Add("M16", "avatar_M16@Idle02");
		dictionary.Add("Ak47", "avatar_M16@Idle02");
		dictionary.Add("AA12", "avatar_XM1014@Idle02");
		dictionary.Add("Galtin", "avatar_Gatlin@Idle02");
		dictionary.Add("RPG", "avatar_RPG@Idle02");
		dictionary.Add("AT4", "avatar_RPG@Idle02");
		dictionary.Add("M202A1", "avatar_RPG@Idle02");
		dictionary.Add("M82A1", "avatar_M82a1@Idle02");
		dictionary.Add("M700", "avatar_M82a1@Idle02");
		dictionary.Add("AS50", "avatar_M82a1@Idle02");
		dictionary.Add("Crabstick", "avatar_crabstick@Idle02");
		dictionary.Add("Knife", "avatar_knife@idle01");
		dictionary.Add("Light_saber", "avatar_ForceKninght@idle02");
		dictionary.Add("Miners_hoe", "avatar_hoe@idle02");
		dictionary.Add("saber", "avatar_saber@Idle02");
		dictionary.Add("CrossBow", "Idle02");
		dictionary.Add("BaseballBat", "idle01_0");
		return dictionary;
	}
}
