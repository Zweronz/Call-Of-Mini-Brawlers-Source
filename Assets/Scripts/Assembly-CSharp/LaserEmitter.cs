using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
	public Transform laserPoint;

	public GameObject laserPrefab;

	private Laser laser;

	public void Emit()
	{
		if (null == laser)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(laserPrefab, laserPoint.position, laserPoint.rotation);
			gameObject.transform.parent = laserPoint;
			laser = gameObject.GetComponent<Laser>();
		}
		laser.gameObject.SetActiveRecursively(true);
		laser.Emit(laserPoint);
	}

	public void Stop()
	{
		if (null != laser)
		{
			laser.gameObject.SetActiveRecursively(false);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
