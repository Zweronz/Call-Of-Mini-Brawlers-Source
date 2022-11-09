public class ZS_Money
{
	private double gold;

	private double tcystal;

	public double Gold
	{
		get
		{
			return gold;
		}
		set
		{
			gold = value;
		}
	}

	public double Tcystal
	{
		get
		{
			return tcystal;
		}
		set
		{
			tcystal = value;
		}
	}

	public ZS_Money()
	{
	}

	public ZS_Money(double gold, double tcystal)
	{
		this.gold = gold;
		this.tcystal = tcystal;
	}
}
