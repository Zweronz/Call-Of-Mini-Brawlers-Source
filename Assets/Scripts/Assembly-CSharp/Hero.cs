using System.Collections.Generic;
using Event;
using UnityEngine;

public class Hero : MonoBehaviour
{
	public Transform weaponPoint;

	public HeroAnimationModel animationModel;

	public HeroPhysicsModel physicsModel;

	public HeroEffectModel effectModel;

	public HeroFaceModel faceModel;

	public HeroColorModel colorModel;

	public LookAtNearestEnemy lookAtNearestEnemyModel;

	private WeaponArsenal weaponArsenal;

	private Gun gun;

	private MeleeWeapon meleeWeapon;

	private bool usingGun = true;

	private bool isAvoid;

	private bool skillLock;

	private bool isDead;

	public bool isGod;

	private bool isInited;

	private HeroData data;

	private float maxHp;

	private float hp;

	private float waitTime = 0.15f;

	private float waittingTimer;

	private bool isWaitting;

	private float damageMedicineRate;

	private ZombieStreetTimer.TimerData timerData;

	private Dictionary<string, WeaponAnimation> weaponAnimations = new Dictionary<string, WeaponAnimation>();

	public HeroData Data
	{
		get
		{
			return data;
		}
	}

	public float MaxHp
	{
		get
		{
			return maxHp;
		}
	}

	public float Hp
	{
		get
		{
			return hp;
		}
	}

	private void Awake()
	{
		CharacterInputJudgment.Instance.character = base.gameObject;
		InitWeaponAnimations();
		weaponArsenal = GameObject.FindGameObjectWithTag("Arsenal").GetComponent<WeaponArsenal>();
		EventCenter.Instance.Register<AvoidOverEvent>(OnAvoidOver);
		EventCenter.Instance.Register<UseItemEvent>(HandleUseItemEvent);
		EventCenter.Instance.Register<AvoidCDOverEvent>(HandleAvoidCDOverEvent);
	}

	public void Instantiate(HeroData data, int level)
	{
		this.data = data;
		maxHp = data.hp + data.hpIncrease * (float)(level - 1);
		AddHp(maxHp);
		SetSpeed(data.moveSpeed);
		Equip(weaponArsenal.MeleeWeapon);
		Remove(meleeWeapon);
		OnSwitch();
		EventCenter.Instance.Publish(this, new LevelChange(Player.Instance.HeroLevel));
		isInited = true;
	}

	public void Revive()
	{
		OnRevive();
	}

	public void LevelUp()
	{
		maxHp = data.hp + data.hpIncrease * (float)(Player.Instance.HeroLevel - 1);
		AddHp(maxHp);
		effectModel.OnLevelUp();
		EventCenter.Instance.Publish(this, new LevelChange(Player.Instance.HeroLevel));
	}

	public void AddHp(float hp)
	{
		if (!isDead)
		{
			this.hp += hp;
			if (this.hp > maxHp)
			{
				this.hp = maxHp;
			}
			EventCenter.Instance.Publish(null, new HeroHPChangeEvent(this.hp, maxHp));
		}
	}

	public void OnBiteHurt(int zombieId, float damage)
	{
		if (isGod)
		{
			return;
		}
		if (!isAvoid)
		{
			effectModel.OnBiteHurt();
			if (!isDead)
			{
				effectModel.PlayeHurtAudio();
			}
		}
		SufferDamage(damage);
	}

	public void OnSalivaHurt(int zombieId, float damage)
	{
		if (isGod)
		{
			return;
		}
		if (!isAvoid)
		{
			effectModel.OnSalivaHurt();
			if (!isDead)
			{
				effectModel.PlayeHurtAudio();
			}
		}
		SufferDamage(damage);
	}

	public void OnHurt(int zombieId, float damage)
	{
		if (!isGod)
		{
			if (!isAvoid && !isDead)
			{
				effectModel.PlayeHurtAudio();
			}
			SufferDamage(damage);
		}
	}

	private void SufferDamage(float damage)
	{
		if (!isDead && !isAvoid)
		{
			hp -= damage;
			if (hp <= 0f)
			{
				hp = 0f;
				OnDead();
			}
			EventCenter.Instance.Publish(null, new HeroHPChangeEvent(hp, maxHp));
		}
	}

	private void OnDead()
	{
		isDead = true;
		CharacterInputJudgment.Instance.Lock();
		lookAtNearestEnemyModel.Lock();
		animationModel.WeaponAnimation.PlayDeadAnimation();
		physicsModel.OnStand();
		if (null != gun)
		{
			Remove(gun);
		}
		if (null != meleeWeapon)
		{
			Remove(meleeWeapon);
		}
	}

	private void OnRevive()
	{
		isDead = false;
		AddHp(MaxHp * 0.5f);
		if (usingGun)
		{
			ReEquip(gun);
		}
		else
		{
			ReEquip(meleeWeapon);
		}
		isAvoid = false;
		CharacterInputJudgment.Instance.Unlock();
		lookAtNearestEnemyModel.Unlock();
		animationModel.WeaponAnimation.PlayReviveAnimation();
		AddBullet(0.5f);
		effectModel.OnRevive();
	}

	public void SetSpeed(float speed)
	{
		physicsModel.speed = speed;
		animationModel.moveSpeed = speed;
	}

	public void SetOriginPoint(Transform point)
	{
		base.transform.position = point.position;
		base.transform.rotation = point.rotation;
	}

	private void InputController_Shoot(CharacterInputJudgment.InputType inputType)
	{
		if (!usingGun)
		{
			Remove(meleeWeapon);
			ReEquip(gun);
		}
		if (gun.Bullets <= 0)
		{
			Gun nextGunHasBullets = weaponArsenal.GetNextGunHasBullets();
			if (null != nextGunHasBullets)
			{
				Switch(nextGunHasBullets);
			}
		}
		if (gun.Bullets > 0 && gun.Judgment.Judge(inputType))
		{
			gun.Attack();
			animationModel.WeaponAnimation.PlayAttackAnimation();
		}
		else if (gun.Bullets <= 0 && inputType == CharacterInputJudgment.InputType.Down)
		{
			effectModel.PlayEmptyFireAudio();
		}
		usingGun = true;
	}

	private void InputController_Switch(CharacterInputJudgment.InputType inputType)
	{
		if (inputType == CharacterInputJudgment.InputType.Up)
		{
			OnSwitch();
		}
	}

	private void InputController_Avoid(CharacterInputJudgment.InputType inputType)
	{
		if (inputType == CharacterInputJudgment.InputType.Up && !isAvoid && !skillLock)
		{
			CharacterInputJudgment.Instance.Lock();
			lookAtNearestEnemyModel.Reback(true);
			animationModel.WeaponAnimation.PlayAvoidAnimation();
			lookAtNearestEnemyModel.Lock();
			physicsModel.OnAvoid();
			isAvoid = true;
			if (usingGun)
			{
				Remove(gun);
			}
			else
			{
				Remove(meleeWeapon);
			}
			skillLock = true;
			EventCenter.Instance.Publish(this, new AvoidCDEvent(Data.skillCD));
		}
	}

	private void InputController_MeleeAttack(CharacterInputJudgment.InputType inputType)
	{
		if (usingGun)
		{
			Remove(gun);
			ReEquip(meleeWeapon);
		}
		if (meleeWeapon.Judgment.Judge(inputType))
		{
			meleeWeapon.Attack();
			animationModel.WeaponAnimation.PlayAttackAnimation();
		}
		usingGun = false;
	}

	private void OnMove()
	{
		animationModel.WeaponAnimation.PlayMoveAnimation();
		physicsModel.OnMove();
	}

	private void OnStand()
	{
		OnStand(0.3f);
	}

	private void OnStand(float fadeLength)
	{
		animationModel.WeaponAnimation.PlayStandAnimation(fadeLength);
		physicsModel.OnStand();
	}

	private void OnTurnAround()
	{
		int num = ((!(base.transform.forward.z > 0f)) ? 1 : (-1));
		base.transform.Rotate(base.transform.up * num * 210f);
	}

	private void Equip(Gun weapon)
	{
		gun = weapon;
		weapon.transform.position = weaponPoint.position;
		weapon.transform.parent = weaponPoint;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.Owner = base.gameObject;
		weapon.muzzle = base.gameObject.transform;
		weapon.flameLightPoint = effectModel.flameLightPoint;
		animationModel.WeaponAnimation = FindAnim(weapon.animationSubTitle);
		weapon.OnEquip();
		OnStand();
		SetSpeed(data.moveSpeed - weapon.Data.mass);
		EventCenter.Instance.Publish(this, new ChangeGunEvent(weapon.Data.icon + "_y"));
		weapon.PublishBulletCountChangeEvent();
	}

	private void ReEquip(Gun weapon)
	{
		weapon.transform.position = weaponPoint.position;
		weapon.transform.parent = weaponPoint;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.Owner = base.gameObject;
		animationModel.WeaponAnimation = FindAnim(weapon.animationSubTitle);
		OnStand();
		SetSpeed(data.moveSpeed - weapon.Data.mass);
	}

	private void Remove(Gun weapon)
	{
		weaponArsenal.Recycle(weapon);
		weapon.OnRemove();
	}

	private void Equip(MeleeWeapon weapon)
	{
		meleeWeapon = weapon;
		weapon.transform.position = weaponPoint.position;
		weapon.transform.parent = weaponPoint;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.Owner = base.gameObject;
		weapon.muzzle = base.gameObject.transform;
		animationModel.WeaponAnimation = FindAnim(weapon.animationSubTitle);
		weapon.OnEquip();
		OnStand();
		SetSpeed(data.moveSpeed - weapon.Data.mass);
	}

	private void ReEquip(MeleeWeapon weapon)
	{
		weapon.transform.position = weaponPoint.position;
		weapon.transform.parent = weaponPoint;
		weapon.transform.localRotation = Quaternion.identity;
		weapon.Owner = base.gameObject;
		animationModel.WeaponAnimation = FindAnim(weapon.animationSubTitle);
		OnStand();
		SetSpeed(data.moveSpeed - weapon.Data.mass);
	}

	private void Remove(MeleeWeapon weapon)
	{
		weaponArsenal.Recycle(weapon);
		weapon.OnRemove();
	}

	private void Switch(Gun weapon)
	{
		if (gun != weapon)
		{
			if (null != gun)
			{
				Remove(gun);
			}
			Equip(weapon);
		}
	}

	private void OnSwitch()
	{
		Gun nextGun = weaponArsenal.NextGun;
		if (gun != nextGun)
		{
			Switch(nextGun);
			Remove(meleeWeapon);
			usingGun = true;
			if (isInited)
			{
				effectModel.PlayeChargeGunAudio();
			}
		}
	}

	private void SniperRifleRealShoot()
	{
		animationModel.SendMessage("SniperRifleRealShoot", SendMessageOptions.DontRequireReceiver);
	}

	private void SniperRifleCancelShoot()
	{
		animationModel.SendMessage("SniperRifleCancelShoot", SendMessageOptions.DontRequireReceiver);
	}

	private void OnLaserGunAttackEnd()
	{
		animationModel.SendMessage("OnLaserGunAttackEnd", SendMessageOptions.DontRequireReceiver);
	}

	private void OnAvoidOver(object sender, AvoidOverEvent evt)
	{
		waitTime = 0.15f;
		OnStand(waitTime);
		CharacterInputJudgment.Instance.Unlock();
		BeginWaitting();
	}

	protected virtual void BeginWaitting()
	{
		waittingTimer = 0f;
		isWaitting = true;
	}

	protected virtual void StopWaitting()
	{
		waittingTimer = 0f;
		isWaitting = false;
	}

	private void Update()
	{
		if (isWaitting)
		{
			waittingTimer += Time.deltaTime;
			if (waittingTimer >= waitTime)
			{
				AvoidOver();
				StopWaitting();
			}
		}
	}

	private void AvoidOver()
	{
		if (usingGun)
		{
			ReEquip(gun);
		}
		else
		{
			ReEquip(meleeWeapon);
		}
		isAvoid = false;
		CharacterInputJudgment.Instance.Unlock();
		lookAtNearestEnemyModel.Unlock();
	}

	private void MeleeAttackOver()
	{
		EventCenter.Instance.Publish(this, new MeleeAttackOverEvent());
	}

	private void RealMeleeAttack()
	{
		meleeWeapon.RealAttack();
	}

	public void AddBullet(float rate)
	{
		weaponArsenal.AddBullet(rate);
		if (null != gun)
		{
			gun.PublishBulletCountChangeEvent();
		}
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<AvoidOverEvent>(OnAvoidOver);
		EventCenter.Instance.Unregister<UseItemEvent>(HandleUseItemEvent);
		EventCenter.Instance.Unregister<AvoidCDOverEvent>(HandleAvoidCDOverEvent);
	}

	private void LockLookAtNearestEnemy()
	{
		lookAtNearestEnemyModel.Lock();
	}

	private void UnlockLookAtNearestEnemy()
	{
		lookAtNearestEnemyModel.Unlock();
	}

	public void UseBulletPackage(BulletPackage bulletPackage)
	{
		AddBullet(bulletPackage.data.rate);
	}

	public void UseDamageMedicine(DamageMedicine damageMedicine)
	{
		if (damageMedicine.data.rate > damageMedicineRate)
		{
			effectModel.OnDamageMedicineBegin();
			weaponArsenal.SubDamage(damageMedicineRate);
			damageMedicineRate = damageMedicine.data.rate;
			weaponArsenal.ChangeDamage(damageMedicineRate);
			if (timerData != null)
			{
				ZombieStreetTimer.RemoveTimer(timerData.ID);
			}
			timerData = new ZombieStreetTimer.TimerData();
			timerData.time = damageMedicine.data.time;
			timerData.handler = DamageMedicineOverTimeHandler;
			ZombieStreetTimer.Instance.AddTimer(timerData);
		}
		else if (damageMedicine.data.time == damageMedicineRate && timerData != null)
		{
			timerData.time += damageMedicine.data.time;
		}
	}

	private void DamageMedicineOverTimeHandler(ZombieStreetTimer.TimerData data)
	{
		effectModel.OnDamageMedicineEnd();
		weaponArsenal.SubDamage(damageMedicineRate);
		damageMedicineRate = 0f;
		ZombieStreetTimer.RemoveTimer(timerData.ID);
		timerData = null;
	}

	public void UseHpMedicine(HpMedicine hpMedicine)
	{
		AddHp(maxHp * hpMedicine.data.rate);
		effectModel.OnUseHpMedicine();
	}

	private void HandleUseItemEvent(object sender, UseItemEvent evt)
	{
		DataCenter.Instance.Items.Find(evt.ItemID).Use(this);
	}

	public void UseAirSupport(AirSupport airSupport)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("AirSupport");
		if (null != gameObject)
		{
			AirSupportEmitter component = gameObject.GetComponent<AirSupportEmitter>();
			component.Fire(airSupport);
		}
	}

	public void AddGold(float gold)
	{
		effectModel.OnGetGold();
		Player.Instance.AddGold((int)(gold * Data.coefficientOfGold));
		EventCenter.Instance.Publish(this, new GoldChangeEvent(Player.Instance.Gold));
	}

	public void AddCrystal(float crystal)
	{
		effectModel.OnGetCrystal();
		Player.Instance.AddCrystal((int)crystal);
	}

	private void InitWeaponAnimations()
	{
		WeaponAnimation[] components = animationModel.GetComponents<WeaponAnimation>();
		if (components != null)
		{
			WeaponAnimation[] array = components;
			foreach (WeaponAnimation weaponAnimation in array)
			{
				weaponAnimations.Add(weaponAnimation.subTitle, weaponAnimation);
			}
		}
	}

	private WeaponAnimation FindAnim(string subTitle)
	{
		if (weaponAnimations.ContainsKey(subTitle))
		{
			return weaponAnimations[subTitle];
		}
		return null;
	}

	private void HandleAvoidCDOverEvent(object sender, AvoidCDOverEvent evt)
	{
		skillLock = false;
	}

	public void Stand()
	{
		OnStand();
	}
}
