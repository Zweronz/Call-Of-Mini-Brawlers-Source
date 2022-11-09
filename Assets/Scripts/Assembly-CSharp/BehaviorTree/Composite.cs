using System.Collections.Generic;

namespace BehaviorTree
{
	public abstract class Composite : Behavior
	{
		protected List<Behavior> behaviors = new List<Behavior>();

		public virtual Composite AddChild(Behavior behavior)
		{
			behaviors.Add(behavior);
			return this;
		}
	}
}
