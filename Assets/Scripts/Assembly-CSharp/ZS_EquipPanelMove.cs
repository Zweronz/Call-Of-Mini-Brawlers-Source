using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Event;
using UnityEngine;

public class ZS_EquipPanelMove : MonoBehaviour
{
	public const string goldIcon = "jinbi";

	public const string tcyIcon = "shuijing";

	private bool rOutFlag = true;

	private bool lOutflag = true;

	private bool rBackFlag = true;

	private bool lBackflag = true;

	private bool canMoveOut = true;

	private float rMoveSpeed = 1f;

	private float lMoveSpeed = 1f;

	public float rMoveDistance = 420f;

	public float lMoveDistance = 350f;

	public float moveTime = 0.1f;

	public TUIFade fade;

	public TUILabel avatarName;

	public GameObject rightMoveOutPanel;

	public GameObject rightMoveInPanel;

	public GameObject leftMoveOutPanel;

	public GameObject leftMoveInPanel;

	public GameObject leftGunMoveInPanel;

	public TUIButtonSelectGroup btnSelectGroup;

	public TUIClipBinder clipBinder;

	public TUIRect clipBinderRect;

	public TUIScrollList scrollList;

	public TUIControl equipControl;

	public TUIControl itemControl;

	public Transform equipPos;

	public Transform itemPos;

	public Transform avatarPos;

	public GameObject[] weaponObjs;

	public GameObject[] itemObjs;

	public GameObject[] avatarObjs;

	private List<ZS_EquipmentInfo> allGun;

	private List<ZS_EquipmentInfo> allSward;

	private List<ZS_EquipmentInfo> equipUsingList;

	private List<ZS_ItemInfo> itemList;

	private List<ZS_ItemInfo> itemUsingList;

	private Func<bool> backEvent;

	public TUILabel goldLabel;

	public TUILabel tcyLabel;

	public TUIButtonClick[] equipBtn;

	public TUIButtonClick[] itemBtn;

	private Vector3 rOutPos;

	private Vector3 rInPos;

	private Vector3 lOutPos;

	private Vector3 lInPos;

	private int size;

	public static ZS_AvatarInfo avatarInfo;

	public static int clickLocation;

	public static int unwiledLocation;

	private ZS_AnimaitonPlay aPlay;

	[CompilerGenerated]
	private static Predicate<ZS_EquipmentInfo> _003C_003Ef__am_0024cache30;

	[CompilerGenerated]
	private static Predicate<ZS_ItemInfo> _003C_003Ef__am_0024cache31;

	private void Awake()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishCurrentAvatarEvent(GetCurrentAvatar));
	}

	private void Start()
	{
		InitialPanelData();
		SetBackEventHandle(LoadNewScene);
	}

	private void GetCurrentAvatar(ZS_AvatarInfo avatar)
	{
		avatarInfo = avatar;
	}

	private void InitialPanelData()
	{
		SetAvatarEquipModel();
		rMoveSpeed = rMoveDistance / moveTime;
		lMoveSpeed = lMoveDistance / moveTime;
		if (null != rightMoveOutPanel)
		{
			rOutPos = rightMoveOutPanel.transform.position;
		}
		if (null != rightMoveInPanel)
		{
			rInPos = rightMoveInPanel.transform.position;
		}
		if (null != leftMoveOutPanel)
		{
			lOutPos = leftMoveOutPanel.transform.position;
		}
		if (null != leftMoveInPanel)
		{
			lInPos = leftMoveInPanel.transform.position;
		}
	}

	private void SetAvatarEquipModel()
	{
		ZS_TUIMisc.DestoryObjectChild(avatarPos);
		GameObject[] array = avatarObjs;
		foreach (GameObject gameObject in array)
		{
			if (!gameObject.name.Equals(avatarInfo.CurrentAvatarPhoto.model))
			{
				continue;
			}
			avatarName.TextID = avatarInfo.CurrentAvatarPhoto.name;
			GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject);
			gameObject2.transform.parent = avatarPos;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			aPlay = gameObject2.GetComponent<ZS_AnimaitonPlay>();
			GameObject[] array2 = weaponObjs;
			foreach (GameObject gameObject3 in array2)
			{
				if (gameObject3.name.Equals(ZS_EquipUsingShow.usingEquipInfo[1].Model))
				{
					GameObject original = gameObject3.transform.GetChild(0).gameObject;
					GameObject gameObject4 = (GameObject)UnityEngine.Object.Instantiate(original);
					gameObject4.transform.parent = aPlay.weapPos;
					gameObject4.transform.localPosition = Vector3.zero;
					gameObject4.transform.localRotation = Quaternion.identity;
					if (gameObject4.name.Equals("Weapon_rifle_gatlin(Clone)"))
					{
						gameObject4.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
					}
					SkinnedMeshRenderer[] componentsInChildren = avatarPos.GetComponentsInChildren<SkinnedMeshRenderer>();
					SkinnedMeshRenderer[] array3 = componentsInChildren;
					foreach (SkinnedMeshRenderer skinnedMeshRenderer in array3)
					{
						skinnedMeshRenderer.gameObject.layer = 8;
					}
					MeshRenderer[] componentsInChildren2 = avatarPos.GetComponentsInChildren<MeshRenderer>();
					MeshRenderer[] array4 = componentsInChildren2;
					foreach (MeshRenderer meshRenderer in array4)
					{
						meshRenderer.gameObject.layer = 8;
					}
					break;
				}
			}
			break;
		}
	}

	public void PlayAnimation()
	{
		if (null != aPlay)
		{
			aPlay.PlayAnimal();
		}
	}

	public void PlayAnimalStart()
	{
		if (null != aPlay)
		{
			aPlay.PlayAnimalStart();
		}
	}

	private void SetBackEventHandle(Func<bool> handle)
	{
		backEvent = handle;
	}

	private void ClearBackEventHandle()
	{
		backEvent = null;
	}

	private int GetClickLoacation(TUIButtonClick[] buttonArray, string name, int defaultLocation)
	{
		for (int i = 0; i < buttonArray.Length; i++)
		{
			if (buttonArray[i].name.Equals(name))
			{
				return i;
			}
		}
		return defaultLocation;
	}

	private void ShowEquipInfo(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			if (canMoveOut)
			{
				canMoveOut = false;
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
				ZS_EquipPanelBtnDelagate component = control.GetComponent<ZS_EquipPanelBtnDelagate>();
				clickLocation = GetClickLoacation(equipBtn, control.name, 2);
				unwiledLocation = clickLocation;
				SetSelectEquipInfo(component.equipInfo);
				StartCoroutine(RightPanelMove());
				StartCoroutine(LeftGunPanelMove());
			}
			clipBinder.SetClipRect(clipBinderRect);
		}
	}

	public void SetSelectEquipInfo(ZS_EquipmentInfo info)
	{
		if (!(null != btnSelectGroup))
		{
			return;
		}
		SetScrollListEquipInfo((info == null) ? 1 : info.Type);
		TUIButtonSelect[] componentsInChildren = btnSelectGroup.GetComponentsInChildren<TUIButtonSelect>();
		ZS_GroupObjectBind component = btnSelectGroup.GetComponent<ZS_GroupObjectBind>();
		TUIButtonClick upGradeBtn = component.upGradeBtn;
		TUIButtonClick unwieldBtn = component.unwieldBtn;
		TUIButtonClick buyBtn = component.buyBtn;
		TUIButtonClick tUIButtonClick = component.equipBtn;
		if (info != null)
		{
			component.equipOwn.SetActiveRecursively(true);
			component.equipUnOwn.SetActiveRecursively(false);
		}
		else
		{
			component.equipUnOwn.SetActiveRecursively(true);
			component.equipOwn.SetActiveRecursively(false);
			List<ZS_EquipmentInfo> list = allGun;
			if (_003C_003Ef__am_0024cache30 == null)
			{
				_003C_003Ef__am_0024cache30 = _003CSetSelectEquipInfo_003Em__5C;
			}
			info = list.Find(_003C_003Ef__am_0024cache30);
			if (info == null)
			{
				info = allGun[0];
			}
		}
		ZS_EquipBtnShowInfo.ShowEquipUpGradeInfo(component, info);
		component.SetAllBtnUnVisable();
		component.equipInfo = info;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			TUIButtonSelect tUIButtonSelect = componentsInChildren[i];
			ZS_EquipBtnShowInfo component2 = tUIButtonSelect.GetComponent<ZS_EquipBtnShowInfo>();
			ZS_EquipmentInfo myEquipInfo = component2.myEquipInfo;
			if (!myEquipInfo.Id.Equals(info.Id))
			{
				continue;
			}
			tUIButtonSelect.SetSelected(true);
			if (info.IsOwn)
			{
				component.ownTopInfo.ShowEquipTopInfo(info.Image, info.Name);
				if (clickLocation != 0 && clickLocation != 1)
				{
					if (info.IsEquiped)
					{
						unwieldBtn.gameObject.SetActiveRecursively(true);
						unwieldBtn.m_PressObj.SetActiveRecursively(false);
						ZS_EquipEventBtnBindInfo component3 = unwieldBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
						component3.equipInfo = info;
						component3.SetUnwieldEventHandle(info.UnwieldCallBack);
					}
					else if (info.IsCanEquip)
					{
						tUIButtonClick.gameObject.SetActiveRecursively(true);
						tUIButtonClick.m_PressObj.SetActiveRecursively(false);
						ZS_EquipEventBtnBindInfo component4 = tUIButtonClick.GetComponent<ZS_EquipEventBtnBindInfo>();
						component4.equipInfo = info;
						component4.SetUnwieldEventHandle(info.EquipCallBack);
					}
					else
					{
						component.equipFail.SetActiveRecursively(true);
						component.equipFailLab.Text = info.EquipCondition;
					}
				}
				if (info.CanUpdate)
				{
					upGradeBtn.gameObject.SetActiveRecursively(true);
					upGradeBtn.m_PressObj.SetActiveRecursively(false);
					ZS_EquipEventBtnBindInfo component5 = upGradeBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
					component5.equipInfo = info;
					component5.SetUpGradeEventHandle(info.UpGradeCallBack);
					ZS_EquipShowPrice componentInChildren = component5.GetComponentInChildren<ZS_EquipShowPrice>();
					componentInChildren.ShowPriceObject(info.UpdateMoney);
				}
				else
				{
					component.updateFail.SetActiveRecursively(true);
				}
			}
			else
			{
				component.unOwnTopInfo.ShowEquipTopInfo(info.Image, info.Name);
				if (info.IsCanBuy)
				{
					buyBtn.gameObject.SetActiveRecursively(true);
					buyBtn.m_PressObj.SetActiveRecursively(false);
					ZS_EquipEventBtnBindInfo component6 = buyBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
					component6.equipInfo = info;
					component6.SetBuyEventHandle(info.BuyCallBack);
					ZS_EquipShowPrice componentInChildren2 = component6.GetComponentInChildren<ZS_EquipShowPrice>();
					componentInChildren2.ShowPriceObject(info.BuyMoney);
				}
				else
				{
					component.buyFail.SetActiveRecursively(true);
					component.buyFailLab.SetFormatText("Text019", info.BuyCondition);
				}
			}
			float num = 0f;
			int num2 = ((size % 2 != 0) ? (size / 2 + 1) : (size / 2));
			num = ((2 * i < size) ? Mathf.Clamp01(Mathf.Floor(i / 2) / (float)num2) : Mathf.Clamp01((Mathf.Floor(i / 2) + 1f) / (float)num2));
			scrollList.ScrollListTo(num);
			break;
		}
	}

	private void SetScrollListEquipInfo(int type)
	{
		List<ZS_EquipmentInfo> list = new List<ZS_EquipmentInfo>();
		int num = 0;
		if (!(null != scrollList))
		{
			return;
		}
		switch (type)
		{
		case 0:
			EventCenter.Instance.Publish(this, new ZS_PublishAllSwardEvent(GetAllSward));
			list = allSward;
			break;
		case 1:
			EventCenter.Instance.Publish(this, new ZS_PublishAllGunEvent(GetAllGun));
			list = allGun;
			break;
		}
		size = list.Count;
		num = ((size % 2 != 0) ? (size / 2 + 1) : (size / 2));
		for (int i = 0; i < num; i++)
		{
			TUIControl tUIControl = UnityEngine.Object.Instantiate(equipControl) as TUIControl;
			TUIButtonSelect[] componentsInChildren = tUIControl.GetComponentsInChildren<TUIButtonSelect>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (size % 2 != 0 && i == num - 1 && j == componentsInChildren.Length - 1)
				{
					componentsInChildren[j].gameObject.SetActiveRecursively(false);
					break;
				}
				ZS_EquipBtnShowInfo component = componentsInChildren[j].GetComponent<ZS_EquipBtnShowInfo>();
				ZS_EquipmentInfo zS_EquipmentInfo = list[i * 2 + j];
				ZS_EquipShowPrice componentInChildren = component.gameObject.GetComponentInChildren<ZS_EquipShowPrice>();
				component.myEquipInfo = zS_EquipmentInfo;
				component.equipIcon.texture = zS_EquipmentInfo.Image;
				if (!zS_EquipmentInfo.IsEquiped)
				{
					component.msEquipped.gameObject.SetActiveRecursively(false);
				}
				if (zS_EquipmentInfo.IsCanBuy)
				{
					component.lockIcon.gameObject.SetActiveRecursively(false);
				}
				if (!zS_EquipmentInfo.IsOwn)
				{
					componentInChildren.ShowPriceObject(zS_EquipmentInfo.BuyMoney);
				}
				else
				{
					componentInChildren.gameObject.SetActiveRecursively(false);
				}
			}
			scrollList.Add(tUIControl);
		}
	}

	public void GetWeapModel(string model)
	{
		GameObject[] array = weaponObjs;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Equals(model))
			{
				ZS_TUIMisc.DestoryObjectChild(equipPos);
				Quaternion localRotation = equipPos.transform.localRotation;
				equipPos.transform.localRotation = new Quaternion(localRotation.x, 0f, 0f, localRotation.w);
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject);
				gameObject2.transform.parent = equipPos;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				SkinnedMeshRenderer[] componentsInChildren = equipPos.GetComponentsInChildren<SkinnedMeshRenderer>();
				SkinnedMeshRenderer[] array2 = componentsInChildren;
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in array2)
				{
					skinnedMeshRenderer.gameObject.layer = 9;
				}
				MeshRenderer[] componentsInChildren2 = equipPos.GetComponentsInChildren<MeshRenderer>();
				MeshRenderer[] array3 = componentsInChildren2;
				foreach (MeshRenderer meshRenderer in array3)
				{
					meshRenderer.gameObject.layer = 9;
				}
				break;
			}
		}
	}

	private void GetAllGun(List<ZS_EquipmentInfo> list)
	{
		allGun = list;
	}

	private void GetAllSward(List<ZS_EquipmentInfo> list)
	{
		allSward = list;
	}

	private void GetUsingEquipInfo(List<ZS_EquipmentInfo> list)
	{
		equipUsingList = list;
	}

	private void ShowItemInfo(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			if (canMoveOut)
			{
				canMoveOut = false;
				ZS_UIAudioManager.PlayAudio(SoundKind.UI_ok);
				ZS_EquipPanelItemDelagate component = control.GetComponent<ZS_EquipPanelItemDelagate>();
				clickLocation = GetClickLoacation(itemBtn, control.name, 0);
				unwiledLocation = clickLocation;
				SetSelectItemInfo(component.itemInfo);
				StartCoroutine(RightPanelMove());
				StartCoroutine(LeftItemPanelMove());
			}
			clipBinder.SetClipRect(clipBinderRect);
		}
	}

	public void GetItemModel(string itemId)
	{
		GameObject[] array = itemObjs;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Equals(itemId))
			{
				ZS_TUIMisc.DestoryObjectChild(itemPos);
				Quaternion localRotation = itemPos.transform.localRotation;
				itemPos.transform.localRotation = new Quaternion(localRotation.x, 0f, 0f, localRotation.w);
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject);
				gameObject2.transform.parent = itemPos;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				MeshRenderer[] componentsInChildren = itemPos.GetComponentsInChildren<MeshRenderer>();
				MeshRenderer[] array2 = componentsInChildren;
				foreach (MeshRenderer meshRenderer in array2)
				{
					meshRenderer.gameObject.layer = 10;
				}
				break;
			}
		}
	}

	public void SetSelectItemInfo(ZS_ItemInfo info)
	{
		if (null != btnSelectGroup)
		{
			SetScrollListItemInfo(info);
		}
	}

	private void GetAllItemInfo(List<ZS_ItemInfo> items)
	{
		itemList = items;
	}

	private void GetUsingItemInfo(List<ZS_ItemInfo> usingItem)
	{
		itemUsingList = usingItem;
	}

	private void SetScrollListItemInfo(ZS_ItemInfo info)
	{
		if (!(null != scrollList))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		EventCenter.Instance.Publish(this, new ZS_PublishAllItemEvent(GetAllItemInfo));
		int count = itemList.Count;
		if (count <= 0)
		{
			return;
		}
		ZS_GroupObjectBind component = btnSelectGroup.GetComponent<ZS_GroupObjectBind>();
		component.SetAllBtnUnVisable();
		if (info == null)
		{
			List<ZS_ItemInfo> list = itemList;
			if (_003C_003Ef__am_0024cache31 == null)
			{
				_003C_003Ef__am_0024cache31 = _003CSetScrollListItemInfo_003Em__5D;
			}
			info = list.Find(_003C_003Ef__am_0024cache31);
		}
		component.itemInfo = info;
		component.itemTopInfo.ShowItemTopInfo(info.Image, info.Name);
		component.itemCount.Text = "YOU OWN " + info.Count;
		component.itemDesc.TextID = info.Desc;
		TUIButtonClick buyItemBtn = component.buyItemBtn;
		TUIButtonClick unwieldItemBtn = component.unwieldItemBtn;
		TUIButtonClick equipItemBtn = component.equipItemBtn;
		ZS_EquipEventBtnBindInfo component2 = buyItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		if (info.canBuy)
		{
			buyItemBtn.gameObject.SetActiveRecursively(true);
			buyItemBtn.m_PressObj.SetActiveRecursively(false);
			component2.itemInfo = info;
			component2.SetBuyItemHandle(info.BuyCallBack);
			ZS_EquipShowPrice componentInChildren = buyItemBtn.GetComponentInChildren<ZS_EquipShowPrice>();
			componentInChildren.ShowPriceObject(info.Money);
		}
		ZS_EquipEventBtnBindInfo component3 = equipItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		ZS_EquipEventBtnBindInfo component4 = unwieldItemBtn.GetComponent<ZS_EquipEventBtnBindInfo>();
		component4.itemInfo = info;
		component3.itemInfo = info;
		component3.SetEquipItemHandle(info.EquipCallBack);
		component4.SetUnwieldItemHandle(info.UnwieldCallBack);
		num = ((count % 2 != 0) ? (count / 2 + 1) : (count / 2));
		for (int i = 0; i < num; i++)
		{
			TUIControl tUIControl = UnityEngine.Object.Instantiate(itemControl) as TUIControl;
			TUIButtonSelect[] componentsInChildren = tUIControl.GetComponentsInChildren<TUIButtonSelect>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (count % 2 != 0 && i == num - 1 && j == componentsInChildren.Length - 1)
				{
					componentsInChildren[j].gameObject.SetActiveRecursively(false);
					break;
				}
				ZS_ItemInfo zS_ItemInfo = itemList[i * 2 + j];
				ZS_EquipBtnItemShowInfo component5 = componentsInChildren[j].GetComponent<ZS_EquipBtnItemShowInfo>();
				ZS_EquipShowPrice componentInChildren2 = componentsInChildren[j].GetComponentInChildren<ZS_EquipShowPrice>();
				componentInChildren2.ShowPriceObject(zS_ItemInfo.Money);
				component5.eInfo = zS_ItemInfo;
				component5.equipIcon.texture = zS_ItemInfo.Image;
				if (!zS_ItemInfo.IsUsing)
				{
					component5.msEquipped.gameObject.SetActiveRecursively(false);
				}
				else
				{
					component5.msEquipped.gameObject.SetActiveRecursively(true);
				}
				if (info.Id.Equals(zS_ItemInfo.Id))
				{
					componentsInChildren[j].SetSelected(true);
					num2 = 2 * i + j;
					if (info.IsUsing)
					{
						unwieldItemBtn.gameObject.SetActiveRecursively(true);
						unwieldItemBtn.m_PressObj.SetActiveRecursively(false);
					}
					else if (info.IsOwn)
					{
						equipItemBtn.gameObject.SetActiveRecursively(true);
						equipItemBtn.m_PressObj.SetActiveRecursively(false);
					}
				}
			}
			scrollList.Add(tUIControl);
		}
		float num3 = 1f * (float)num2 / (float)count;
		num3 = ((!(num3 >= 2f / (float)num)) ? Mathf.Clamp01(Mathf.Floor(num2 / 2) / (float)num) : Mathf.Clamp01((Mathf.Floor(num2 / 2) + 1f) / (float)num));
		scrollList.ScrollListTo(num3);
	}

	private IEnumerator RightPanelMove()
	{
		float rDistance = 0f;
		while (true)
		{
			if (rOutFlag)
			{
				rDistance += rMoveSpeed * Time.deltaTime;
				if (rDistance >= rMoveDistance)
				{
					ZS_TUIMisc.SetPosition(rightMoveOutPanel, rOutPos.x + rMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					rOutPos = rightMoveOutPanel.transform.position;
					rDistance = 0f;
					rOutFlag = false;
				}
				else
				{
					ZS_TUIMisc.SetPosition(rightMoveOutPanel, rOutPos.x + rDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				rDistance += rMoveSpeed * Time.deltaTime;
				if (rDistance >= rMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(rightMoveInPanel, rInPos.x - rDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(rightMoveInPanel, rInPos.x - rMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		rInPos = rightMoveInPanel.transform.position;
		rOutFlag = true;
	}

	private IEnumerator LeftItemPanelMove()
	{
		float lDistance = 0f;
		while (true)
		{
			if (lOutflag)
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x - lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					lOutPos = leftMoveOutPanel.transform.position;
					lDistance = 0f;
					lOutflag = false;
				}
				else
				{
					ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x - lDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(leftMoveInPanel, lInPos.x + lDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(leftMoveInPanel, lInPos.x + lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		lInPos = leftMoveInPanel.transform.position;
		lOutflag = true;
		SetBackEventHandle(BackItemSameScene);
	}

	private IEnumerator LeftGunPanelMove()
	{
		float lDistance = 0f;
		while (true)
		{
			if (lOutflag)
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x - lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					lOutPos = leftMoveOutPanel.transform.position;
					lDistance = 0f;
					lOutflag = false;
				}
				else
				{
					ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x - lDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(leftGunMoveInPanel, lInPos.x + lDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(leftGunMoveInPanel, lInPos.x + lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		lInPos = leftGunMoveInPanel.transform.position;
		lOutflag = true;
		SetBackEventHandle(BackGunSameScene);
	}

	private IEnumerator RightPanelBackIn()
	{
		float rDistance = 0f;
		while (true)
		{
			if (rBackFlag)
			{
				rDistance += rMoveSpeed * Time.deltaTime;
				if (rDistance >= rMoveDistance)
				{
					ZS_TUIMisc.SetPosition(rightMoveInPanel, rInPos.x + rMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					rInPos = rightMoveInPanel.transform.position;
					rDistance = 0f;
					rBackFlag = false;
					if (null != scrollList)
					{
						ZS_GroupObjectBind btnGroupShow = btnSelectGroup.GetComponent<ZS_GroupObjectBind>();
						btnGroupShow.SetAllBtnUnVisable();
						scrollList.Clear(true);
					}
				}
				else
				{
					ZS_TUIMisc.SetPosition(rightMoveInPanel, rInPos.x + rDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				rDistance += rMoveSpeed * Time.deltaTime;
				if (rDistance >= rMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(rightMoveOutPanel, rOutPos.x - rDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(rightMoveOutPanel, rOutPos.x - rMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		rOutPos = rightMoveOutPanel.transform.position;
		rBackFlag = true;
	}

	private IEnumerator LeftItemPanelBackIn()
	{
		float lDistance = 0f;
		while (true)
		{
			if (lBackflag)
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					ZS_TUIMisc.SetPosition(leftMoveInPanel, lInPos.x - lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					lInPos = leftMoveInPanel.transform.position;
					lDistance = 0f;
					lBackflag = false;
				}
				else
				{
					ZS_TUIMisc.SetPosition(leftMoveInPanel, lInPos.x - lDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x + lDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x + lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		lOutPos = leftMoveOutPanel.transform.position;
		lBackflag = true;
		canMoveOut = true;
	}

	private IEnumerator LeftGunPanelBackIn()
	{
		float lDistance = 0f;
		while (true)
		{
			if (lBackflag)
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					ZS_TUIMisc.SetPosition(leftGunMoveInPanel, lInPos.x - lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
					lInPos = leftGunMoveInPanel.transform.position;
					lDistance = 0f;
					lBackflag = false;
				}
				else
				{
					ZS_TUIMisc.SetPosition(leftGunMoveInPanel, lInPos.x - lDistance, ZS_TUIMisc.Arrangement.Horizontal);
					yield return true;
				}
			}
			else
			{
				lDistance += lMoveSpeed * Time.deltaTime;
				if (lDistance >= lMoveDistance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x + lDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(leftMoveOutPanel, lOutPos.x + lMoveDistance, ZS_TUIMisc.Arrangement.Horizontal);
		lOutPos = leftMoveOutPanel.transform.position;
		lBackflag = true;
		canMoveOut = true;
	}

	private void TriggerBackEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			backEvent();
		}
	}

	private bool LoadNewScene()
	{
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
		if (null != fade)
		{
			fade.FadeOut(ZS_TUIMisc.indexScene);
		}
		else
		{
			Application.LoadLevel(ZS_TUIMisc.indexScene);
		}
		return true;
	}

	private bool BackItemSameScene()
	{
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
		RefreshDatas();
		SetBackEventHandle(LoadNewScene);
		StartCoroutine(RightPanelBackIn());
		StartCoroutine(LeftItemPanelBackIn());
		StartCoroutine(PlayAnimalContinue(1f));
		return true;
	}

	private bool BackGunSameScene()
	{
		ZS_UIAudioManager.PlayAudio(SoundKind.UI_no_back);
		RefreshDatas();
		SetBackEventHandle(LoadNewScene);
		StartCoroutine(RightPanelBackIn());
		StartCoroutine(LeftGunPanelBackIn());
		StartCoroutine(PlayAnimalContinue(1f));
		return true;
	}

	private IEnumerator PlayAnimalContinue(float waitSeconds)
	{
		yield return new WaitForSeconds(waitSeconds);
		ZS_AnimaitonPlay animationPlay = avatarPos.GetComponentInChildren<ZS_AnimaitonPlay>();
		animationPlay.PlayAnimalStart();
	}

	private void RefreshDatas()
	{
		EventCenter.Instance.Publish(this, new ZS_PublishUsingEquipEvent(GetUsingEquipInfo, ZS_EquipUsingShow.count));
		EventCenter.Instance.Publish(this, new ZS_PublishUsingItemEvent(GetUsingItemInfo, ZS_EquipUsingItemShow.count));
		ZS_EquipUsingShow componentInChildren = rightMoveOutPanel.GetComponentInChildren<ZS_EquipUsingShow>();
		ZS_EquipUsingItemShow componentInChildren2 = rightMoveOutPanel.GetComponentInChildren<ZS_EquipUsingItemShow>();
		ZS_EquipUsingShow.usingEquipInfo = equipUsingList;
		ZS_EquipUsingItemShow.usingItemInfo = itemUsingList;
		componentInChildren2.SetUsingItemInfo();
		componentInChildren.SetUsingEquipInfo();
		ZS_AnimaitonPlay componentInChildren3 = avatarPos.GetComponentInChildren<ZS_AnimaitonPlay>();
		if (!(null != componentInChildren3) || equipUsingList[1] == null)
		{
			return;
		}
		ZS_TUIMisc.DestoryObjectChild(componentInChildren3.weapPos);
		componentInChildren3.PlayAnimal();
		GameObject[] array = weaponObjs;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Equals(equipUsingList[1].Model))
			{
				GameObject original = gameObject.transform.GetChild(0).gameObject;
				GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(original);
				gameObject2.transform.parent = componentInChildren3.weapPos;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				if (gameObject2.name.Equals("Weapon_rifle_gatlin(Clone)"))
				{
					gameObject2.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
				}
				MeshRenderer[] componentsInChildren = avatarPos.GetComponentsInChildren<MeshRenderer>();
				MeshRenderer[] array2 = componentsInChildren;
				foreach (MeshRenderer meshRenderer in array2)
				{
					meshRenderer.gameObject.layer = 8;
				}
				SkinnedMeshRenderer[] componentsInChildren2 = avatarPos.GetComponentsInChildren<SkinnedMeshRenderer>();
				SkinnedMeshRenderer[] array3 = componentsInChildren2;
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in array3)
				{
					skinnedMeshRenderer.gameObject.layer = 8;
				}
			}
		}
	}

	[CompilerGenerated]
	private static bool _003CSetSelectEquipInfo_003Em__5C(ZS_EquipmentInfo einfo)
	{
		return !einfo.IsOwn;
	}

	[CompilerGenerated]
	private static bool _003CSetScrollListItemInfo_003Em__5D(ZS_ItemInfo itemInfo)
	{
		return !itemInfo.IsUsing;
	}
}
