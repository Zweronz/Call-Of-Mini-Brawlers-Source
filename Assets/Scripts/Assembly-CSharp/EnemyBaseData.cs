using System;
using System.Collections.Generic;

[Serializable]
public class EnemyBaseData
{
	public int id;

	public string name;

	public float coefficientOfGold;

	public float coefficientOfExp;

	public float coefficientOfHp;

	public float coefficientOfDamage;

	public float attackRange;

	public float speed;

	public List<int> appearType;

	public float frictionA;

	public float stiff;

	public float deceleration;

	public float timeOfRestoration;

	public string modelName;

	public float attackPreparationTime;

	public Dictionary<int, float> resistance;
}
