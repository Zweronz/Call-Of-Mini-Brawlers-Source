using Event;
using UnityEngine;

public class RevivePanel : MonoBehaviour
{
	public string iapId;

	public SpecialIAPCrystalLabel2 label;

	public TUISlider slider;

	public float showTime;

	private float timer;

	public Vector3 showDis;

	public Vector3 disapearDis;

	public ReviveIAPPanel iapPanel;

	private bool isShowing;

	private int crystal = 1;

	private ReviveIAPData data;

	public int Crystal
	{
		get
		{
			return crystal;
		}
	}

	private void Awake()
	{
		EventCenter.Instance.Register<ShowReviveEvent>(HandleShowReviveEvent);
		EventCenter.Instance.Publish(null, new GetReviveIAPEvent(iapId, HandleReviveIAPData));
	}

	private void OnDestroy()
	{
		EventCenter.Instance.Unregister<ShowReviveEvent>(HandleShowReviveEvent);
	}

	private void HandleReviveIAPData(ReviveIAPData data)
	{
		this.data = data;
		iapPanel.Init(data, this);
	}

	private void Start()
	{
		Hide();
	}

	private void Update()
	{
		if (isShowing)
		{
			timer += Time.deltaTime;
			if (timer <= showTime)
			{
				slider.sliderValue = 1f - timer / showTime;
			}
			else
			{
				Skip();
			}
		}
	}

	private void Show(int crystal)
	{
		this.crystal = crystal;
		label.SetCrystal(crystal);
		timer = 0f;
		Resume();
	}

	public void Resume()
	{
		base.transform.position = showDis;
		isShowing = true;
	}

	private void Hide()
	{
		base.transform.position = disapearDis;
		isShowing = false;
	}

	private void Skip()
	{
		Hide();
		EventCenter.Instance.Publish(null, new SkipReviveEvent());
	}

	private void HandleShowReviveEvent(object sender, ShowReviveEvent evt)
	{
		Show(evt.Crystal);
	}

	private void HandleReviveBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3 && this.data != null && this.data.reviveAction != null)
		{
			if (!this.data.reviveAction(Crystal))
			{
				iapPanel.Show();
			}
			Hide();
		}
	}

	private void HandleSkipBtn(TUIControl control, int eventType, float wp, float lp, object data)
	{
		if (eventType == 3)
		{
			Skip();
		}
	}
}
