using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour
{
	public bool open = false;
	public bool locked = false;
	public bool crystalGathered = false;
	public int portalNumber;

	GameObject closeSound;
	GameObject portalshared;
	GameObject objLocked;
	GameObject crystal;
	obj_teleport teleport;


	void Start ()
	{
		closeSound = transform.FindChild("portalshared").FindChild("closeSound").gameObject;

		portalshared = transform.FindChild("portalshared").FindChild("shared").gameObject;

		objLocked = transform.FindChild("lockedportal").FindChild("locked").gameObject;

		crystal = transform.FindChild("portalshared").FindChild("portalcrystal").gameObject;

		teleport = transform.GetComponentInChildren<obj_teleport>();
	}

	void Update ()
	{
		if (open)
		{
			portalshared.SetActive(true);
			closeSound.SetActive(false);
		}
		else
		{
			portalshared.SetActive(false);
			closeSound.SetActive(true);
		}

		if (locked)
		{
			objLocked.SetActive(true);
		}
		else
		{
			objLocked.SetActive(false);
		}

		if (open && !locked)
			teleport.enabled = true;
		else
			teleport.enabled = false;

		if (crystalGathered)
			crystal.SetActive(true);
		else
			crystal.SetActive(false);
	}

	public void TogglePortal ()
	{
		open = !open;
	}
}