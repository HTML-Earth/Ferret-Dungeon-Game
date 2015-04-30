using UnityEngine;
using System.Collections;

public class p_animation : MonoBehaviour
{
	Animator anim;

	public bool sprinting = false;
	public bool moving = false;
	public bool attacking = false;
	public bool dead = false;

	void Start ()
	{
		anim = transform.GetComponent<Animator>();
	}

	void Update ()
	{
		anim.SetBool("sprinting", sprinting);
		anim.SetBool("moving", moving);
		anim.SetBool("attacking", attacking);
		anim.SetBool("dead", dead);
	}
}