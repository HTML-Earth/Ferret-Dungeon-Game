﻿using UnityEngine;
using System.Collections;

public class obj_crystal : MonoBehaviour
{
	public GameObject linkedPortal;
	
	portal thePortal;
	
	BoxCollider boxCol;
	MeshRenderer mesh;
	Light light;
	bool done;
	float waitTime;
	
	void Start ()
	{
		thePortal = linkedPortal.GetComponent<portal>();
		
		boxCol = gameObject.GetComponent<BoxCollider>();
		mesh = gameObject.GetComponent<MeshRenderer>();
		light = gameObject.GetComponentInChildren<Light>();

		waitTime = Time.time;
	}
	
	void Update ()
	{
		if (done && Time.time - waitTime > 1)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player")
		{
			thePortal.crystalGathered = true;
			
			boxCol.enabled = false;
			mesh.enabled = false;
			light.enabled = false;

			waitTime = Time.time;

			GetComponent<AudioSource>().Play();
			done = true;
		}
	}
}