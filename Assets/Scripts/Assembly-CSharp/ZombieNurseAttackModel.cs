using UnityEngine;

public class ZombieNurseAttackModel : ZombieAttackModel
{
	public Transform emitterPoint;

	public GameObject salivaPrefab;

	public Zombie self;

	public Target target;

	public void Emit(Vector3 endPoint)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(salivaPrefab, base.transform.position, Quaternion.identity);
		Saliva component = gameObject.GetComponent<Saliva>();
		component.zombieId = self.Data.id;
		component.coefficientOfDamage = self.CoefficientOfDamage;
		component.Spout(emitterPoint, endPoint);
	}

	public override void BeginAttack()
	{
	}

	public override void EndAttack()
	{
	}

	private void OnSalivaEmit()
	{
		Emit(target.target.transform.position);
	}
}
