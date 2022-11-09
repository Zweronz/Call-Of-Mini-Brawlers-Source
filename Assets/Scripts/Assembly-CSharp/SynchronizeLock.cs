using System;

public class SynchronizeLock
{
	private int callTimes = 1;

	private int lockNum;

	private Action callback;

	public void SetCallback(Action callback)
	{
		this.callback = callback;
	}

	public void SetCallTimes(int times)
	{
		callTimes = times;
	}

	public void Lock(uint num = 1)
	{
		lockNum += (int)num;
	}

	public void Unlock(uint num = 1)
	{
		lockNum -= (int)num;
		if (lockNum <= 0)
		{
			lockNum = 0;
			if (callTimes > 0 && callback != null)
			{
				callback();
				callTimes--;
			}
		}
	}
}
