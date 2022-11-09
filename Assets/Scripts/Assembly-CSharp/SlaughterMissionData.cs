using System;
using System.Collections.Generic;

[Serializable]
public class SlaughterMissionData : MissionData
{
	[Serializable]
	public class SlaughterTargetData
	{
		public int id;

		public int count;

		public float rise;
	}

	public List<SlaughterTargetData> targets;
}
