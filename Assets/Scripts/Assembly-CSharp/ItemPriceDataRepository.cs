using System.Collections.Generic;

public class ItemPriceDataRepository
{
	private Dictionary<string, Dictionary<int, ItemPriceData>> datas = new Dictionary<string, Dictionary<int, ItemPriceData>>();

	public void Initialize(IDataReadWriteModel dataReadWriteModel)
	{
		datas.Clear();
		ItemPriceData[] array = dataReadWriteModel.Deserialize<ItemPriceData[]>();
		if (array == null)
		{
			return;
		}
		ItemPriceData[] array2 = array;
		foreach (ItemPriceData itemPriceData in array2)
		{
			if (!datas.ContainsKey(itemPriceData.itemId))
			{
				datas.Add(itemPriceData.itemId, new Dictionary<int, ItemPriceData>());
			}
			datas[itemPriceData.itemId].Add(itemPriceData.heroLevel, itemPriceData);
		}
	}

	public ItemPriceData Find(string itemId, int level)
	{
		return datas[itemId][level];
	}
}
