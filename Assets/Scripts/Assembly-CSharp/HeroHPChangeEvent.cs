public class HeroHPChangeEvent
{
	public float Current { get; private set; }

	public float Max { get; private set; }

	public HeroHPChangeEvent(float current, float max)
	{
		Current = current;
		Max = max;
	}
}
