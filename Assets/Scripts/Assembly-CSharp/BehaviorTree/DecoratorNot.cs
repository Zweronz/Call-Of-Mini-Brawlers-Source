namespace BehaviorTree
{
	public class DecoratorNot : Decorator
	{
		public DecoratorNot(Behavior behavior)
			: base(behavior)
		{
		}

		public override Status Update()
		{
			Status status = behavior.Tick();
			if (status != Status.Failure)
			{
				return Status.Success;
			}
			return Status.Failure;
		}
	}
}
