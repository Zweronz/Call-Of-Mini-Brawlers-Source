using Event;
using UnityEngine;

public class GameUIHeroBar : MonoBehaviour
{
	public TUIMeshSprite icon;

	public TUILabel level;

	private void Awake()
	{
		EventCenter.Instance.Register<LevelChange>(HandleLevelChange);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<LevelChange>(HandleLevelChange);
	}

	private void Start()
	{
		SetIcon(DataCenter.Instance.Heros.Find(Player.Instance.CurrentHero.id).icon);
	}

	private void SetIcon(string icon)
	{
		this.icon.texture = icon;
	}

	private void SetLevel(int level)
	{
		this.level.Text = "Lv " + level;
	}

	private void HandleLevelChange(object sender, LevelChange evt)
	{
		SetLevel(evt.Level);
	}
}
