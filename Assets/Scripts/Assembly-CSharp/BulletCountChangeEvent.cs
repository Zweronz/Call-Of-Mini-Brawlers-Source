public class BulletCountChangeEvent
{
	public int Current { get; private set; }

	public int Max { get; private set; }

	public BulletCountChangeEvent(int current, int max)
	{
		Current = current;
		Max = max;
	}
}
