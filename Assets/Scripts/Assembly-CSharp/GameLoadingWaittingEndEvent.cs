public class GameLoadingWaittingEndEvent
{
	public bool ClearAction { get; private set; }

	public GameLoadingWaittingEndEvent(bool clearAction)
	{
		ClearAction = clearAction;
	}
}
