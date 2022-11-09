public class ExpChangeEvent
{
	public double Current { get; private set; }

	public double Max { get; private set; }

	public ExpChangeEvent(double current, double max)
	{
		Current = current;
		Max = max;
	}
}
