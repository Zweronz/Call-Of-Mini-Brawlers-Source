using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TUIInputHandler : MonoBehaviour
{
	[Serializable]
	public class TUIInputHandle
	{
		public TUIInputHandleModel tui;

		public int priority;

		public bool penetrate;
	}

	public TUIInputHandle[] handles;

	private List<TUIInputHandle> mHandles = new List<TUIInputHandle>();

	[CompilerGenerated]
	private static Predicate<TUIInputHandle> _003C_003Ef__am_0024cache2;

	[CompilerGenerated]
	private static Comparison<TUIInputHandle> _003C_003Ef__am_0024cache3;

	public void AddHandle(TUIInputHandle handle)
	{
		List<TUIInputHandle> list = ((handles != null) ? new List<TUIInputHandle>(handles) : new List<TUIInputHandle>());
		list.Add(handle);
		handles = list.ToArray();
		mHandles.Add(handle);
		Sort();
	}

	private void Sort()
	{
		List<TUIInputHandle> list = mHandles;
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = _003CSort_003Em__58;
		}
		list.RemoveAll(_003C_003Ef__am_0024cache2);
		List<TUIInputHandle> list2 = mHandles;
		if (_003C_003Ef__am_0024cache3 == null)
		{
			_003C_003Ef__am_0024cache3 = _003CSort_003Em__59;
		}
		list2.Sort(_003C_003Ef__am_0024cache3);
	}

	private void Awake()
	{
		if (handles != null)
		{
			mHandles.AddRange(handles);
		}
		Sort();
	}

	private void Update()
	{
		TUIInput[] input = TUIInputManager.GetInput();
		TUIInput[] array = input;
		foreach (TUIInput input2 in array)
		{
			for (int j = 0; j < mHandles.Count && (!mHandles[j].tui.HandleInput(input2) || mHandles[j].penetrate); j++)
			{
			}
		}
	}

	[CompilerGenerated]
	private static bool _003CSort_003Em__58(TUIInputHandle handle)
	{
		return handle.tui == null;
	}

	[CompilerGenerated]
	private static int _003CSort_003Em__59(TUIInputHandle handle1, TUIInputHandle handle2)
	{
		if (handle1.priority < handle2.priority)
		{
			return 1;
		}
		if (handle1.priority == handle2.priority)
		{
			return 0;
		}
		return -1;
	}
}
