using System.Collections.Generic;

namespace BehaviorTree
{
	public class Sequence : Composite
	{
		public override Status Update()
		{
			return Update(behaviors);
		}

		protected Status Update(List<Behavior> behaviors)
		{
			foreach (Behavior behavior in behaviors)
			{
				Status status = behavior.Tick();
				if (status != Status.Success)
				{
					return status;
				}
			}
			return Status.Success;
		}
	}
}
