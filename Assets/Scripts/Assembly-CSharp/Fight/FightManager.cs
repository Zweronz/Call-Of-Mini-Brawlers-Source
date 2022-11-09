using System.Collections.Generic;

namespace Fight
{
	public class FightManager
	{
		private static FightManager instance;

		private List<IFightBehavior> ready4Produce = new List<IFightBehavior>();

		private List<IFightBehavior> produced = new List<IFightBehavior>();

		public static FightManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new FightManager();
				}
				return instance;
			}
		}

		public void Add(IFightBehavior fightBehavior, bool produceImmediately = false)
		{
			if (produceImmediately)
			{
				fightBehavior.Execute();
				produced.Add(fightBehavior);
			}
			else
			{
				ready4Produce.Add(fightBehavior);
			}
		}

		public void Update()
		{
			if (ready4Produce.Count <= 0)
			{
				return;
			}
			foreach (IFightBehavior item in ready4Produce)
			{
				item.Execute();
			}
			produced.AddRange(ready4Produce);
			ready4Produce.Clear();
		}
	}
}
