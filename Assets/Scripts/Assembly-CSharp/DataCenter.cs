public class DataCenter
{
	private static DataCenter instance;

	public static DataCenter Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new DataCenter();
			}
			return instance;
		}
	}

	public GunRepository Guns { get; private set; }

	public MeleeWeaponRepository MeleeWeapons { get; private set; }

	public RefreshRuleRepository Rules { get; private set; }

	public EnemyBaseDataRepository BaseEnemies { get; private set; }

	public EnemyBaseHpDmgDataRepository BaseEnemiesHpDmg { get; private set; }

	public MissionRepository Missions { get; private set; }

	public HeroDataRepository Heros { get; private set; }

	public HeroLevelUpExpDataRepository HeroLevelUpDatas { get; private set; }

	public AchievementRepository Achievements { get; private set; }

	public ItemDataRepository Items { get; private set; }

	public IAPDataRepository IAPs { get; private set; }

	public SpecialIAPDataRepository SpecialIAPs { get; private set; }

	public ItemPriceDataRepository ItemPrices { get; private set; }

	public Crystal2GoldDataRepository Crystal2GoldData { get; private set; }

	public MapPointDataRepository MapPoints { get; private set; }

	private DataCenter()
	{
		Guns = new GunRepository();
		MeleeWeapons = new MeleeWeaponRepository();
		Rules = new RefreshRuleRepository();
		BaseEnemies = new EnemyBaseDataRepository();
		BaseEnemiesHpDmg = new EnemyBaseHpDmgDataRepository();
		Missions = new MissionRepository();
		Heros = new HeroDataRepository();
		Achievements = new AchievementRepository();
		HeroLevelUpDatas = new HeroLevelUpExpDataRepository();
		Items = new ItemDataRepository();
		IAPs = new IAPDataRepository();
		ItemPrices = new ItemPriceDataRepository();
		Crystal2GoldData = new Crystal2GoldDataRepository();
		MapPoints = new MapPointDataRepository();
		SpecialIAPs = new SpecialIAPDataRepository();
		Guns.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/guns", false));
		MeleeWeapons.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/meleeWeapons", false));
		Rules.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/rules", false));
		BaseEnemies.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/enemies", false));
		BaseEnemiesHpDmg.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/ebhd", false));
		Missions.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/missions", false));
		Heros.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/heros", false));
		Achievements.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/achievements", false));
		HeroLevelUpDatas.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/hlud", false));
		Items.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/items", false));
		IAPs.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/iap", false));
		ItemPrices.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/ips", false));
		Crystal2GoldData.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/c2g", false));
		MapPoints.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/mp", false));
		SpecialIAPs.Initialize(new DataReadWriteModel(BinaryDataReadWrite.Instance, "Data/siap", false));
	}
}
