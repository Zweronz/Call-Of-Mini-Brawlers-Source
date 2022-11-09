using UnityEngine;

public class CreateEnemy
{
	public int ID { get; private set; }

	public Vector3 Position { get; private set; }

	public Quaternion Rotation { get; private set; }

	public CreateEnemy(int id, Vector3 position, Quaternion rotation)
	{
		ID = id;
		Position = position;
		Rotation = rotation;
	}
}
