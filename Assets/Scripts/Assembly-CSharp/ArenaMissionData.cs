using System;
using System.Collections.Generic;

[Serializable]
public class ArenaMissionData : MissionData
{
	[Serializable]
	public class EnemyRate
	{
		public int meter;

		public float goldRate;

		public float expRate;

		public float hpRate;

		public float damageRate;

		public float attackRangeRate;

		public float speedRate;

		public float frictionARate;

		public float stiffRate;

		public float decelerationRate;

		public float timeOfRestorationRate;

		public float attackPreparationTimeRate;
	}

	[Serializable]
	public class MeterRule
	{
		public EnemyRate enemyRate;

		public float interval;

		public int maxZombies;

		public List<string> refreshRules;
	}

	public List<MeterRule> meterRules;

	[NonSerialized]
	private bool isChanged;

	[NonSerialized]
	private Dictionary<int, MeterRule> meterRulesDic;

	public Dictionary<int, MeterRule> MeterRules
	{
		get
		{
			if (!isChanged)
			{
				if (meterRulesDic == null)
				{
					meterRulesDic = new Dictionary<int, MeterRule>();
				}
				if (meterRules != null)
				{
					foreach (MeterRule meterRule in meterRules)
					{
						meterRulesDic.Add(meterRule.enemyRate.meter, meterRule);
					}
				}
				isChanged = true;
			}
			return meterRulesDic;
		}
	}
}
