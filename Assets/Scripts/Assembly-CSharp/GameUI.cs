using Event;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	[SerializeField]
	protected GameUIHP hpModel;

	[SerializeField]
	protected GameUIBullet bulletModel;

	[SerializeField]
	protected GameUIGold goldModel;

	[SerializeField]
	protected GameUIItem itemModel;

	[SerializeField]
	protected GameUIExp expModel;

	[SerializeField]
	protected GameUIAvoidCD avoidCDModel;

	[SerializeField]
	protected TUIMeshSprite gunIcon;

	[SerializeField]
	protected Animation switchWeaponAnim;

	private void Start()
	{
		itemModel.Instantiate();
	}

	private void Awake()
	{
		EventCenter.Instance.Register<HeroHPChangeEvent>(HandleHeroHPChangeEvent);
		EventCenter.Instance.Register<BulletCountChangeEvent>(HandleBulletCountChangeEvent);
		EventCenter.Instance.Register<ChangeGunEvent>(HandleChangeGunEvent);
		EventCenter.Instance.Register<GoldChangeEvent>(HandleGoldChangeEvent);
		EventCenter.Instance.Register<ExpChangeEvent>(HandleExpChangeEvent);
		EventCenter.Instance.Register<AvoidCDEvent>(HandleAvoidCDEvent);
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<HeroHPChangeEvent>(HandleHeroHPChangeEvent);
		EventCenter.Instance.Unregister<BulletCountChangeEvent>(HandleBulletCountChangeEvent);
		EventCenter.Instance.Unregister<ChangeGunEvent>(HandleChangeGunEvent);
		EventCenter.Instance.Unregister<GoldChangeEvent>(HandleGoldChangeEvent);
		EventCenter.Instance.Unregister<ExpChangeEvent>(HandleExpChangeEvent);
		EventCenter.Instance.Unregister<AvoidCDEvent>(HandleAvoidCDEvent);
	}

	private void HandleHeroHPChangeEvent(object sender, HeroHPChangeEvent evt)
	{
		hpModel.SetHP(evt.Current, evt.Max);
	}

	private void HandleBulletCountChangeEvent(object sender, BulletCountChangeEvent evt)
	{
		bulletModel.SetBullet(evt.Current, evt.Max);
	}

	private void HandleChangeGunEvent(object sender, ChangeGunEvent evt)
	{
		gunIcon.texture = evt.Icon;
		gunIcon.ForceUpdate();
		switchWeaponAnim.Stop();
		switchWeaponAnim.Play();
	}

	private void HandleGoldChangeEvent(object sender, GoldChangeEvent evt)
	{
		goldModel.SetGold((int)evt.Gold);
	}

	private void HandleExpChangeEvent(object sender, ExpChangeEvent evt)
	{
		expModel.SetExp(evt.Current, evt.Max);
	}

	private void HandleAvoidCDEvent(object sender, AvoidCDEvent evt)
	{
		avoidCDModel.StartCD(evt.Time);
	}
}
