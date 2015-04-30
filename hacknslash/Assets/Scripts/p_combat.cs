using UnityEngine;
using System.Collections;

public class p_combat : MonoBehaviour
{
	public GameObject meleeDamage;

	public float attackDamage = 10;
	public float attackRange = 5;

	public bool disabled = false;

	p_movement p_move;
	p_animation anim;
	ui_game main_ui;

	Transform currentTarget;
	unit_health targetHealth;

	void Start ()
	{
		anim = GetComponent<p_animation>();
		p_move = GetComponent<p_movement>();
		main_ui = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<ui_game>();
	}

	void Update ()
	{
		if (Input.GetButton("Attack") && !disabled && !main_ui.isPaused && p_move.mouseMovement)
		{
			anim.attacking = true;
		}


		if (Input.GetAxis("Attack") < 0 && !disabled && !main_ui.isPaused && !p_move.mouseMovement)
		{
			anim.attacking = true;
		}
	}

	void Attack()
	{
		Vector3 damagePos = transform.position + transform.forward * attackRange + transform.up * 5;

		GameObject dmg = Instantiate(meleeDamage, damagePos, Quaternion.identity) as GameObject;
		melee_damage dmgScript = dmg.GetComponent<melee_damage>();

		dmg.transform.parent = transform;
		dmgScript.damage = attackDamage;

		Destroy(dmg.gameObject, 0.3f);

		if (!Input.GetButton("Attack") && p_move.mouseMovement)
			anim.attacking = false;

		if (Input.GetAxis("Attack") > 0 && !p_move.mouseMovement)
			anim.attacking = false;
	}
}
