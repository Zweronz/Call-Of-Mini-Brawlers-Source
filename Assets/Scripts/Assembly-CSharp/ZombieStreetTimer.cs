using System;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStreetTimer : MonoBehaviour
{
	public class TimerData
	{
		public float time;

		public bool ingoreTimeScale;

		public int invokeTimes = 1;

		public object data;

		public TimerHandler handler;

		private string id = string.Empty;

		private bool isInitialized;

		private float initializeTime;

		public string ID
		{
			get
			{
				return id;
			}
		}

		public TimerData()
		{
			id = Guid.NewGuid().ToString();
		}

		public void Invoke(float time, float ingoreTime)
		{
			if (!isInitialized)
			{
				Initialize();
			}
			if (invokeTimes == 0)
			{
				return;
			}
			float num = time;
			if (ingoreTimeScale)
			{
				num = ingoreTime;
			}
			this.time -= num;
			if (this.time <= 0f)
			{
				if (handler != null)
				{
					handler(this);
				}
				invokeTimes--;
				if (invokeTimes != 0)
				{
					this.time = initializeTime;
				}
			}
		}

		private void Initialize()
		{
			initializeTime = time;
			isInitialized = true;
		}
	}

	public delegate void TimerHandler(TimerData data);

	private static ZombieStreetTimer instance = null;

	private float realtimeSinceStartup;

	private static Dictionary<string, TimerData> datas = new Dictionary<string, TimerData>();

	private List<TimerData> tempList = new List<TimerData>();

	public static ZombieStreetTimer Instance
	{
		get
		{
			if (null == instance)
			{
				instance = Create();
			}
			return instance;
		}
	}

	public void AddTimer(TimerData data)
	{
		if (!datas.ContainsKey(data.ID))
		{
			datas.Add(data.ID, data);
		}
	}

	public static void RemoveTimer(string id)
	{
		if (datas.ContainsKey(id))
		{
			datas.Remove(id);
		}
	}

	public static void RemoveTimer(TimerData data)
	{
		RemoveTimer(data.ID);
	}

	private void Start()
	{
		realtimeSinceStartup = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		float time = GetTime();
		float ingoreTime = GetIngoreTime();
		tempList.Clear();
		tempList.AddRange(datas.Values);
		foreach (TimerData temp in tempList)
		{
			temp.Invoke(time, ingoreTime);
		}
	}

	private float GetTime()
	{
		return Time.deltaTime;
	}

	private float GetIngoreTime()
	{
		float result = Time.realtimeSinceStartup - realtimeSinceStartup;
		realtimeSinceStartup = Time.realtimeSinceStartup;
		return result;
	}

	private static ZombieStreetTimer Create()
	{
		GameObject gameObject = new GameObject("Timer");
		return gameObject.AddComponent<ZombieStreetTimer>();
	}

	private void OnDestroy()
	{
		datas.Clear();
	}
}
