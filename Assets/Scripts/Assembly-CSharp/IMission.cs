using System.Collections.Generic;

public interface IMission
{
	int Type { get; }

	string ID { get; }

	int Priority { get; }

	float RefreshZombieInterval { get; }

	float SceneLength { get; }

	List<string> RefreshRules { get; }

	bool NeedSpecialRefresh { get; }

	float SpecialRefreshZombieInterval { get; }

	List<string> SpecialRefreshRules { get; }

	MissionState State { get; }

	string Icon { get; }

	string DescId { get; }

	bool IsAvailable(int level);

	void Reset(bool resetInfo = true);

	void Start();

	void Initialize(int level);

	void InitializeUI();

	List<object> GetDescData(int level);
}
