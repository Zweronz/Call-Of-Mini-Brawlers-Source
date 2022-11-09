public class UseItemEvent
{
	public string ItemID { get; private set; }

	public UseItemEvent(string itemID)
	{
		ItemID = itemID;
	}
}
