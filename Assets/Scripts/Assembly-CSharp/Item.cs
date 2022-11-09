using System;

[Serializable]
public abstract class Item<TItemData> : IItem where TItemData : ItemData
{
	public TItemData data;

	public ItemData BaseData
	{
		get
		{
			return data;
		}
	}

	public abstract void Use(Hero hero);

	public virtual bool UseItemSfx()
	{
		return true;
	}
}
