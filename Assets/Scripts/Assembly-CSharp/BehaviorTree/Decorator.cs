namespace BehaviorTree
{
	public abstract class Decorator : Behavior
	{
		protected Behavior behavior;

		protected Decorator(Behavior behavior)
		{
			this.behavior = behavior;
		}
	}
}
