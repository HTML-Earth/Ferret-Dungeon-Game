using UnityEngine;
using System.Collections;

public class d20 : MonoBehaviour
{
	public GameObject[] dSides;

	public GameObject[] portals;

	public GameObject endPortal;

	portal endportal_script;

	public Transform platform;

	int currentSide;

	bool slotted = false;

	public AudioSource[] sounds;
	public AudioSource roll;
	public AudioSource slot;

	void Start ()
	{
		sounds = GetComponents<AudioSource>();
		roll = sounds[0];
		slot = sounds[1];

		dSides = GameObject.FindGameObjectsWithTag("D20Side");
		portals = GameObject.FindGameObjectsWithTag("Portal");

		endportal_script = endPortal.GetComponent<portal>();

		InvokeRepeating("LimitedUpdate", 0, 0.5F);
	}

	void LimitedUpdate()
	{
		if ( Vector3.Distance( new Vector3(transform.position.x,0,transform.position.z),new Vector3(platform.position.x,0,platform.position.z)) > 1)
		{
			slotted = false;

			CheckSide();

			endportal_script.open = false;
		}
		else if (slotted)
		{
			//Nothing...
		}
		else
		{
			for (int i = 0; i < dSides.Length; i++)
			{
				d20_side d20side = dSides[i].GetComponent<d20_side>();

				dSides[i].GetComponent<Light>().enabled = true;
			}

			for(int i = 0; i < portals.Length; i++)
			{
				portal portal_script = portals[i].GetComponent<portal>();

				endportal_script.open = true;

				portal_script.open = false;
			}
			slotted = true;

			slot.Play();
		}
	}

	void CheckSide ()
	{
		float tempHeight = -1000;
		int tempSide = 0;

		for(int i = 0; i < dSides.Length; i++)
		{
			d20_side d20side = dSides[i].GetComponent<d20_side>();

			if (dSides[i].transform.position.y > tempHeight)
			{
				tempHeight = dSides[i].transform.position.y;
				tempSide = d20side.number;
			}
		}
		if (tempSide != currentSide)
		{
			currentSide = tempSide;
			//Debug.Log("current side is " + currentSide);

			for (int i = 0; i < dSides.Length; i++)
			{
				d20_side d20side = dSides[i].GetComponent<d20_side>();

				if (d20side.number == currentSide)
					dSides[i].GetComponent<Light>().enabled = true;
				else
					dSides[i].GetComponent<Light>().enabled = false;
			}

			ManagePortals();
		}
	}

	void ManagePortals()
	{
		for(int i = 0; i < portals.Length; i++)
		{
			portal portal_script = portals[i].GetComponent<portal>();

			if (portal_script.portalNumber == currentSide)
				portal_script.open = true;
			else
				portal_script.open = false;
		}
		roll.Play();
	}
}
