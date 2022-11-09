public interface IItem
{
	ItemData BaseData { get; }

	void Use(Hero hero);

	bool UseItemSfx();
}
