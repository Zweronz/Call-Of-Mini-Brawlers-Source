public class RefreshChestEvent
{
	public enum Direction
	{
		Backward = 0,
		Current = 1,
		Forward = 2
	}

	public Direction Dire { get; private set; }

	public RefreshChestEvent(Direction dire)
	{
		Dire = dire;
	}
}
