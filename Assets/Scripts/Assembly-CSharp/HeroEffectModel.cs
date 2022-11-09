using UnityEngine;

public class HeroEffectModel : MonoBehaviour
{
	public Transform flameLightPoint;

	public Transform bloodPoint;

	public GameObject bloodPrefab;

	public GameObject salivaPrefab;

	public Transform levelUpPoint;

	public GameObject levelUpPrefab;

	public ITAudioEvent hurtAudio;

	public ITAudioEvent emptyFireAudio;

	public ITAudioEvent chargeGunAudio;

	public ITAudioEvent levelUpAudio;

	public GameObject getGoldPrefab;

	public GameObject getCrystalPrefab;

	public GameObject hpMedicinePrefab;

	public GameObject revivePrefab;

	public GameObject damageMedicinePrefab;

	private GameObject currentDamageMedicine;

	public void OnBiteHurt()
	{
		if (null != bloodPrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(bloodPrefab, bloodPoint.position, Quaternion.identity);
			gameObject.transform.parent = bloodPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnSalivaHurt()
	{
		if (null != salivaPrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(salivaPrefab, bloodPoint.position, Quaternion.identity);
			gameObject.transform.parent = bloodPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnLevelUp()
	{
		if (null != levelUpPrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(levelUpPrefab, levelUpPoint.position, Quaternion.identity);
			gameObject.transform.parent = levelUpPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
		PlayLevelUpAudio();
	}

	public void OnDamageMedicineBegin()
	{
		if (null != damageMedicinePrefab)
		{
			currentDamageMedicine = (GameObject)Object.Instantiate(damageMedicinePrefab, levelUpPoint.position, Quaternion.identity);
			currentDamageMedicine.transform.parent = levelUpPoint;
			currentDamageMedicine.transform.localPosition = Vector3.zero;
			currentDamageMedicine.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnDamageMedicineEnd()
	{
		if (null != currentDamageMedicine)
		{
			Object.Destroy(currentDamageMedicine);
		}
	}

	public void OnGetGold()
	{
		if (null != getGoldPrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(getGoldPrefab, levelUpPoint.position, Quaternion.identity);
			gameObject.transform.parent = levelUpPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnGetCrystal()
	{
		if (null != getCrystalPrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(getCrystalPrefab, levelUpPoint.position, Quaternion.identity);
			gameObject.transform.parent = levelUpPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnUseHpMedicine()
	{
		if (null != hpMedicinePrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(hpMedicinePrefab, levelUpPoint.position, Quaternion.identity);
			gameObject.transform.parent = levelUpPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void OnRevive()
	{
		if (null != revivePrefab)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(revivePrefab, levelUpPoint.position, Quaternion.identity);
			gameObject.transform.parent = levelUpPoint;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
		}
	}

	public void PlayEmptyFireAudio()
	{
		emptyFireAudio.transform.position = base.transform.position;
		emptyFireAudio.Trigger();
	}

	public void PlayeHurtAudio()
	{
		hurtAudio.transform.position = base.transform.position;
		hurtAudio.Trigger();
	}

	public void PlayeChargeGunAudio()
	{
		chargeGunAudio.transform.position = base.transform.position;
		chargeGunAudio.Trigger();
	}

	public void PlayLevelUpAudio()
	{
		levelUpAudio.transform.position = base.transform.position;
		levelUpAudio.Trigger();
	}
}
