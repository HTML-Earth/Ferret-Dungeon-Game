using UnityEngine;
using System.Collections;

public class ai_skullspider : MonoBehaviour
{
	e_animation anim;
	GameObject player;
	//This comment is necessary.
	public string state = "Guard";
	public float aggroRange = 40;
	public float forgetRange = 80;
	public float attackRange = 7;
	public float attackDamage = 5;
	public float speed = 20;
	public bool dead = false;

	public GameObject meleeDamage;

	Vector3 lastPosition;

	SphereCollider scol;

	bool stillAttacking = false;
		
	void Start ()
	{
		anim = GetComponent<e_animation>();
		scol = GetComponent<SphereCollider>();
		
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void FixedUpdate ()
	{
		//shitty collision detection fix
		if (!dead)
			//transform.position += transform.up * 0.01f;

		if (dead)
		{
			anim.dead = true;
			Destroy(GetComponent<Rigidbody>());
			Destroy(scol);
		}
		else if ( state == "Attack" )
		{
			if ( Vector3.Distance(transform.position,player.transform.position) > attackRange + 1  && !stillAttacking)
			{
				lastPosition = player.transform.position;

				state = "Chase";
				anim.moving = true;
				anim.attacking = false;
			}
			
			transform.LookAt(player.transform.position);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		else if ( state == "Chase" )
		{
			if ( Vector3.Distance(transform.position,player.transform.position) < attackRange)
			{
				state = "Attack";
				anim.moving = false;
				anim.attacking = true;
			}

			if ( Vector3.Distance(transform.position,player.transform.position) > forgetRange )
			{
				state = "Guard";
				anim.moving = false;
			}

			CheckLineOfSight();

			transform.LookAt(lastPosition);
			GetComponent<Rigidbody>().velocity = transform.forward * speed;
		}
		else
		{
			if ( Vector3.Distance(transform.position,player.transform.position) < aggroRange)
			{
				lastPosition = player.transform.position;

				state = "Chase";
				anim.moving = true;
			}
		}
		
	}

	void CheckLineOfSight()
	{
		Vector3 heading = transform.position - player.transform.position;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;
		
		Debug.DrawLine(transform.position + Vector3.up * 5, -direction, Color.white);
		
		int layerMask = 1 << 9;
		layerMask = ~layerMask;

		RaycastHit hit;
		if (Physics.Raycast(transform.position + Vector3.up * 5, -direction, out hit, forgetRange, layerMask))
		{
			if (hit.transform.tag == "Player")
			{
				Debug.Log("hit player (" + hit.transform.name +")");
				lastPosition = player.transform.position;
				Debug.DrawLine(transform.position, lastPosition, Color.green);
			}
			else
			{
				Debug.Log(hit.transform.name);
				Debug.DrawLine(transform.position + Vector3.up * 5, hit.point, Color.yellow);
				Debug.DrawLine(lastPosition, lastPosition + Vector3.up * 100, Color.yellow);
			}
			
		}
		else
		{
			Debug.DrawLine(lastPosition, lastPosition + Vector3.up * 100, Color.red);
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
	}
}