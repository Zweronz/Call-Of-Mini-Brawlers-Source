public class CrystalChangedEvent
{
	public double Crystal { get; private set; }

	public CrystalChangedEvent(double crystal)
	{
		Crystal = crystal;
	}
}
