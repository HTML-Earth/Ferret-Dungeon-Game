using UnityEngine;
using System.Collections;

public class audio_play : MonoBehaviour
{
	
	void Start ()
	{

	}

	public void Footstep()
	{
		GetComponent<AudioSource>().Play();
	}
	
}