using System.Collections;
using UnityEngine;

public class ZS_AvatarMove : MonoBehaviour
{
	public GameObject avatarPos;

	public float moveTime = 0.5f;

	public float distance = 1f;

	private bool flag;

	private float moveSpeed = 1f;

	private Vector3 pos = Vector3.zero;

	private void Start()
	{
		moveSpeed = distance / moveTime;
		pos = avatarPos.transform.position;
		StartCoroutine(AvatarMove());
	}

	private void Update()
	{
	}

	private IEnumerator AvatarMove()
	{
		float moveDistance = 0f;
		while (true)
		{
			if (flag)
			{
				moveDistance += moveSpeed * Time.deltaTime;
				if (moveDistance > distance)
				{
					ZS_TUIMisc.SetPosition(avatarPos, pos.x - distance, ZS_TUIMisc.Arrangement.Horizontal);
					yield break;
				}
				ZS_TUIMisc.SetPosition(avatarPos, pos.x - moveDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
			else
			{
				moveDistance += moveSpeed * Time.deltaTime;
				if (moveDistance >= distance)
				{
					break;
				}
				ZS_TUIMisc.SetPosition(avatarPos, pos.x + moveDistance, ZS_TUIMisc.Arrangement.Horizontal);
				yield return true;
			}
		}
		ZS_TUIMisc.SetPosition(avatarPos, pos.x + distance, ZS_TUIMisc.Arrangement.Horizontal);
	}
}
