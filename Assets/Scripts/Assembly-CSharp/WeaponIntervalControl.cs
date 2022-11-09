using System;
using UnityEngine;

public class WeaponIntervalControl : MonoBehaviour
{
	public delegate void IntervalHandle();

	private IntervalHandle beginInterval;

	private IntervalHandle endInterval;

	private float intervalTimer;

	private bool isInterval;

	[SerializeField]
	protected float interval;

	public float Interval
	{
		get
		{
			return interval;
		}
		set
		{
			interval = value;
		}
	}

	public void AddBeginIntervalHandle(IntervalHandle handle)
	{
		beginInterval = (IntervalHandle)Delegate.Combine(beginInterval, handle);
	}

	public void RemoveBeginIntervalHandle(IntervalHandle handle)
	{
		beginInterval = (IntervalHandle)Delegate.Remove(beginInterval, handle);
	}

	public void AddEndIntervalHandle(IntervalHandle handle)
	{
		endInterval = (IntervalHandle)Delegate.Combine(endInterval, handle);
	}

	public void RemoveEndIntervalHandle(IntervalHandle handle)
	{
		endInterval = (IntervalHandle)Delegate.Remove(endInterval, handle);
	}

	public virtual void BeginInterval()
	{
		intervalTimer = 0f;
		isInterval = true;
		if (beginInterval != null)
		{
			beginInterval();
		}
	}

	public virtual void EndInterval()
	{
		intervalTimer = 0f;
		isInterval = false;
		if (endInterval != null)
		{
			endInterval();
		}
	}

	private void Update()
	{
		if (isInterval)
		{
			intervalTimer += Time.deltaTime;
			if (intervalTimer >= Interval)
			{
				EndInterval();
			}
		}
	}
}
