public class DestroyFriendChestEvent
{
	public GameCenterPlayer Friend { get; private set; }

	public DestroyFriendChestEvent(GameCenterPlayer friend)
	{
		Friend = friend;
	}
}
