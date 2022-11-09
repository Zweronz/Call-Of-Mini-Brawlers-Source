public class ZS_PublishMissRewardEvent
{
	public delegate void GetMissRewardInfo(ZS_MissRewardInfo info);

	public GetMissRewardInfo MissRewardInfoDel { get; private set; }

	public ZS_PublishMissRewardEvent(GetMissRewardInfo missRewardInfo)
	{
		MissRewardInfoDel = missRewardInfo;
	}
}
