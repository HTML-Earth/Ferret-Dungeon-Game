using UnityEngine;
using System.Collections;

public class obj_teleport : MonoBehaviour
{
	public bool enabled;

	public GameObject exit;
	Vector3 destination;

	obj_teleport exitScript;
	float cooldown;

	void Start ()
	{
		cooldown = Time.time;

		exitScript = exit.GetComponent<obj_teleport>();

		destination = exit.transform.position + exit.transform.forward * -10;
	}

	void OnTriggerEnter(Collider traveler)
	{
		if (traveler.tag == "Player" && enabled && Time.time-cooldown > 0.5f)
		{
			traveler.transform.position = destination;
			unit_health playerhp = traveler.GetComponent<unit_health>();

			exitScript.cooldown = Time.time;

			playerhp.spawnPosition = destination;
		}
	}

	void Update ()
	{
		
	}
}
