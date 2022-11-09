namespace BehaviorTree
{
	public abstract class Behavior
	{
		private Status status;

		public abstract Status Update();

		public virtual Status Tick()
		{
			if (status == Status.Invalid)
			{
				OnInitialize();
			}
			status = Update();
			if (status != Status.Running)
			{
				OnTerminate(status);
			}
			return status;
		}

		protected virtual void OnInitialize()
		{
		}

		protected virtual void OnTerminate(Status status)
		{
		}
	}
}
