using UnityEngine;

public class ZS_AvatarGroupBtnShow : MonoBehaviour
{
	public TUIButtonClick useBtn;

	public TUIButtonClick buyBtn;

	public GameObject unableBtn;

	public GameObject hpShow;

	public GameObject unlockShow;

	public TUILabel unlockCondition;

	public Transform avatarPos;

	public GameObject[] avatarModels;

	public GameObject[] meleeModels;

	private ZS_AvatarPhotoInfo info;

	private void Start()
	{
		info = ZS_TopInfomation.avatar.CurrentAvatarPhoto;
		FreshAvatarModel(info);
	}

	public void HideAvatarPanelBtn()
	{
		useBtn.gameObject.SetActiveRecursively(false);
		buyBtn.gameObject.SetActiveRecursively(false);
		unableBtn.SetActiveRecursively(false);
		hpShow.SetActiveRecursively(false);
		unlockShow.SetActiveRecursively(false);
	}

	public void FreshAvatarModel(ZS_AvatarPhotoInfo aPhotoInfo)
	{
		ZS_TUIMisc.DestoryObjectChild(avatarPos);
		GameObject[] array = avatarModels;
		foreach (GameObject gameObject in array)
		{
			if (!gameObject.name.Equals(aPhotoInfo.model))
			{
				continue;
			}
			GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
			GameObject gameObject3 = InitialMeleeWeapon(aPhotoInfo.MeleeWeapon);
			ZS_AnimaitonPlay componentInChildren = gameObject2.GetComponentInChildren<ZS_AnimaitonPlay>();
			Animation[] lightAnimation = GetLightAnimation(gameObject3);
			if (lightAnimation != null)
			{
				ZS_AnimationChange componentInChildren2 = gameObject2.GetComponentInChildren<ZS_AnimationChange>();
				if (null != componentInChildren2)
				{
					componentInChildren2.subAnim = lightAnimation;
				}
			}
			gameObject2.transform.parent = avatarPos;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			if (null != gameObject3)
			{
				gameObject3.transform.parent = componentInChildren.weapPos;
				gameObject3.transform.localPosition = Vector3.zero;
				if ("Light_saber(Clone)".Equals(gameObject3.name))
				{
					gameObject3.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
				}
				else
				{
					gameObject3.transform.localEulerAngles = Vector3.zero;
				}
			}
			componentInChildren.PlayAnimal(aPhotoInfo.MeleeWeapon);
			SkinnedMeshRenderer[] componentsInChildren = avatarPos.GetComponentsInChildren<SkinnedMeshRenderer>();
			SkinnedMeshRenderer[] array2 = componentsInChildren;
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in array2)
			{
				skinnedMeshRenderer.gameObject.layer = 8;
			}
			MeshRenderer[] componentsInChildren2 = avatarPos.GetComponentsInChildren<MeshRenderer>();
			MeshRenderer[] array3 = componentsInChildren2;
			foreach (MeshRenderer meshRenderer in array3)
			{
				meshRenderer.gameObject.layer = 8;
			}
			break;
		}
	}

	private GameObject InitialMeleeWeapon(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		GameObject[] array = meleeModels;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Equals(name))
			{
				return Object.Instantiate(gameObject) as GameObject;
			}
		}
		return null;
	}

	private Animation[] GetLightAnimation(GameObject obj)
	{
		if (null != obj)
		{
			return obj.GetComponentsInChildren<Animation>();
		}
		return null;
	}
}
