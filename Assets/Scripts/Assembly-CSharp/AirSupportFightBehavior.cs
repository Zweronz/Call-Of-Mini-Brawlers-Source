using System.Collections.Generic;
using Fight;
using UnityEngine;

public class AirSupportFightBehavior : IFightBehavior
{
	private AirSupport airSupport;

	private List<Zombie> victims;

	private List<Destructible> destructibleObjs;

	public AirSupportFightBehavior(AirSupport airSupport, params GameObject[] victims)
	{
		this.victims = new List<Zombie>();
		destructibleObjs = new List<Destructible>();
		this.airSupport = airSupport;
		if (victims == null)
		{
			return;
		}
		foreach (GameObject gameObject in victims)
		{
			if (gameObject.tag == "Zombie")
			{
				this.victims.Add(gameObject.GetComponent<Zombie>());
				destructibleObjs.Add(null);
			}
			else if (gameObject.tag == "Destructible")
			{
				this.victims.Add(null);
				destructibleObjs.Add(gameObject.GetComponent<Destructible>());
			}
		}
	}

	public void Execute()
	{
		int num = 0;
		foreach (Zombie victim in victims)
		{
			if (null != victim)
			{
				victim.OnHurt(airSupport, CalculateHurt(victim, num));
			}
			else if (null != destructibleObjs[num])
			{
				destructibleObjs[num].OnHurt(airSupport, CalculateHurt(destructibleObjs[num], num));
			}
			num++;
		}
	}

	private float CalculateHurt(Zombie zombie, int index)
	{
		return airSupport.data.damage;
	}

	private float CalculateHurt(Destructible destructibleObj, int index)
	{
		return airSupport.data.damage;
	}
}
