using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveControl : MonoBehaviour
{
	[SerializeField]
	protected float moveSpeed;

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
		set
		{
			if (value != moveSpeed)
			{
				moveSpeed = value;
				NotifyWhenSpeedChanged(moveSpeed);
			}
		}
	}

	private void Start()
	{
		NotifyWhenSpeedChanged(MoveSpeed);
	}

	protected virtual void OnMove()
	{
		base.GetComponent<Rigidbody>().velocity = base.transform.forward * moveSpeed;
	}

	protected virtual void OnStand()
	{
		base.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void NotifyWhenSpeedChanged(float speed)
	{
		SendMessage("OnChangeMoveSpeed", speed, SendMessageOptions.DontRequireReceiver);
	}
}
