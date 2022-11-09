using System;
using System.Collections.Generic;

namespace BehaviorTree
{
	public class WeightRandomSequence : Sequence
	{
		protected List<float> weights = new List<float>();

		private Random random = new Random(default(Guid).GetHashCode());

		public override Composite AddChild(Behavior behavior)
		{
			return AddChild(behavior, 0f);
		}

		public virtual Composite AddChild(Behavior behavior, float weight)
		{
			weights.Add(weight);
			return base.AddChild(behavior);
		}

		public override Status Update()
		{
			List<Behavior> list = new List<Behavior>(behaviors);
			list.Sort(RandomSort);
			return Update(list);
		}

		protected int RandomSort(Behavior behavior1, Behavior behavior2)
		{
			int index = behaviors.IndexOf(behavior1);
			int index2 = behaviors.IndexOf(behavior2);
			float num = weights[index];
			float num2 = weights[index2];
			float num3 = (float)random.NextDouble();
			return (num3 * (num + num2) <= num) ? 1 : (-1);
		}
	}
}
