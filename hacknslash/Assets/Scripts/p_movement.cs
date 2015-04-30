using UnityEngine;
using System.Collections;

public class p_movement : MonoBehaviour
{
	public bool mouseMovement = true;

	public float speed = 100;
	public float rayDistance = 1000f;

	public bool rooted = false;
	public bool disabled = false;
	public bool deadForReal = false;

	p_animation anim;

	public LayerMask layerMask;

	void Start ()
	{
		layerMask = ~layerMask;

		anim = GetComponent<p_animation>();
	}

	void FixedUpdate ()
	{
		if (mouseMovement && !disabled)
			MouseMove();
		else if (!disabled)
			ControllerMove();

		//shitty collision detection fix
		//transform.position += transform.up * 0.01f;
	}


	void MouseMove()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, rayDistance, layerMask)) 
		{
			transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));

			if (Input.GetMouseButton(0) && !rooted)
			{
				Vector3 direction = new Vector3(hit.point.x - transform.position.x,0,hit.point.z - transform.position.z);
				GetComponent<Rigidbody>().velocity = direction.normalized * speed;
				anim.moving = true;
			}   
			else
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				anim.moving = false;
			}
		}
	}

	void ControllerMove()
	{
		if (rooted)
		{
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
				transform.LookAt (new Vector3(Input.GetAxis("Horizontal") * 1000,transform.position.y,Input.GetAxis("Vertical") * 1000));
			anim.moving = false;
		}
		else
		{
			GetComponent<Rigidbody>().velocity = ( new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical") ) * speed );

			if ( GetComponent<Rigidbody>().velocity.magnitude > 2)
			{
				transform.LookAt (transform.position + new Vector3( GetComponent<Rigidbody>().velocity.x * 1000,transform.position.y,GetComponent<Rigidbody>().velocity.z * 1000 ));
				anim.moving = true;
			}
			else
				anim.moving = false;
		}
	}
}