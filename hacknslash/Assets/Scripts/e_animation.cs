using UnityEngine;
using System.Collections;

public class e_animation : MonoBehaviour
{
	Animator anim;

	public bool moving = false;
	public bool attacking = false;
	public bool dead = false;
	
	void Start ()
	{
		anim = transform.GetComponent<Animator>();
	}
	
	void Update ()
	{
		anim.SetBool("moving", moving);
		anim.SetBool("attacking", attacking);
		anim.SetBool("dead", dead);
	}
}