using UnityEngine;
using System.Collections;

public class melee_damage : MonoBehaviour
{
	public float damage = 10;

	SphereCollider sphereCol;
	bool done;
	float waitTime;

	void Start ()
	{
		sphereCol = gameObject.GetComponent<SphereCollider>();
		waitTime = Time.time;
	}

	void Update ()
	{
		if (done && Time.time - waitTime > 1)
			Destroy(gameObject);
	}

	void OnTriggerStay(Collider target)
	{
		if (transform.parent.gameObject.layer.Equals(8) && target.gameObject.layer.Equals(9) || transform.parent.gameObject.layer.Equals(9) && target.gameObject.layer.Equals(8))
		{
			unit_health targetHealth = target.gameObject.GetComponent<unit_health>();
			targetHealth.Damage(damage);

			Debug.Log(transform.parent.name + " -> " + damage + " DMG -> " + target.name);

			sphereCol.enabled = false;
			done = true;
		}
	}
}