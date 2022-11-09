using System;
using System.Collections.Generic;

[Serializable]
public class MissionData
{
	public int type;

	public string id;

	public int priority;

	public float refreshZombieInterval;

	public float sceneLength;

	public int minLevel;

	public int maxLevel;

	public int interval;

	public List<string> refreshRules;

	public float specialRefreshZombieInterval;

	public List<string> specialRefrashRules;

	public string descId;

	public string icon;
}
