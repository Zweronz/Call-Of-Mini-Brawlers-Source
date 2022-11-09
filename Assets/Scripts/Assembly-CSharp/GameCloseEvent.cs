public class GameCloseEvent
{
	public bool RerollPlayer { get; private set; }

	public GameCloseEvent(bool rerollPlayer)
	{
		RerollPlayer = rerollPlayer;
	}
}
