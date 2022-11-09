public class ArenaGameOverEvent
{
	public ArenaMissionDetail MissionDetail { get; private set; }

	public ArenaGameOverEvent(ArenaMissionDetail missionDetail)
	{
		MissionDetail = missionDetail;
	}
}
