using System.Collections.Generic;
using Event;
using UnityEngine;

public class GameUIItem : MonoBehaviour
{
	[SerializeField]
	protected TUIScrollList list;

	[SerializeField]
	protected GameObject itemBtnPrefab;

	[SerializeField]
	protected TUIRect clipRect;

	[SerializeField]
	protected ITAudioEvent useItemAudio;

	[HideInInspector]
	public List<TUIControl> buttons;

	public void Instantiate()
	{
		list.Clear(true);
		List<string> items = Player.Instance.Items;
		if (items == null)
		{
			return;
		}
		foreach (string item in items)
		{
			if (!string.IsNullOrEmpty(item))
			{
				TUIControl control = CreateItemBtn(item);
				list.Add(control);
				buttons.Add(control);
			}
		}
	}

	public void HandleUseItem(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType != 3)
		{
			return;
		}
		string text = control == null ? data as string : control.data as string;
		int itemCount = Player.Instance.GetItemCount(text);
		if (itemCount > 0)
		{
			IItem item = DataCenter.Instance.Items.Find(text);
			if (item.UseItemSfx())
			{
				useItemAudio.Trigger();
			}
			Hero component = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
			item.Use(component);
			Player.Instance.AddItem(text, -1);
			MyFlurry.UseItem(text, Player.Instance.GameLevel);
			EventCenter.Instance.Publish(this, new UseItemEvent(text));
			SetGameUIItemBtn(text, control.GetComponent<GameUIItemBtn>());
		}
	}

	private TUIControl CreateItemBtn(string itemId)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(itemBtnPrefab);
		GameUIItemBtn componentInChildren = gameObject.GetComponentInChildren<GameUIItemBtn>();
		if (null != componentInChildren)
		{
			SetGameUIItemBtn(itemId, componentInChildren);
		}
		TUIButtonClick componentInChildren2 = gameObject.GetComponentInChildren<TUIButtonClick>();
		componentInChildren2.invokeOnEvent = true;
		componentInChildren2.invokeObject = base.gameObject;
		componentInChildren2.componentName = GetType().Name;
		componentInChildren2.invokeFunctionName = "HandleUseItem";
		componentInChildren2.data = itemId;
		TUIClipBinder component = gameObject.GetComponent<TUIClipBinder>();
		component.SetClipRect(clipRect);
		return component;
	}

	private void SetGameUIItemBtn(string itemId, GameUIItemBtn btn)
	{
		string icon = DataCenter.Instance.Items.Find(itemId).BaseData.icon + "_y";
		int itemCount = Player.Instance.GetItemCount(itemId);
		btn.Set(icon, itemCount);
	}
}
