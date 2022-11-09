public interface IAchievement
{
	string ID { get; }

	string NextID { get; }

	int Type { get; }

	float Gold { get; }

	float Crystal { get; }

	string ItemId { get; }

	int ItemCount { get; }

	string Desc { get; }

	AchievementState State { get; }

	float Progress { get; }

	void Initialize();

	void Clear();

	void Process();

	void GetReward();
}
