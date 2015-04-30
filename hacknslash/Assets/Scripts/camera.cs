using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
	public bool mouseZoom = true;

	public Transform target;

	public float distance;
	public float height;
	public float targetHeight;

	public float fov = 80;

	float newHeight;

	void Start ()
	{
		newHeight = height;
	}

	void Update ()
	{
		GetComponent<Camera>().fieldOfView = fov;

		transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z + distance);
		transform.LookAt(new Vector3 (target.position.x, target.position.y + targetHeight, target.position.z));

		if (mouseZoom)
			newHeight -= Input.GetAxis("Mouse ScrollWheel") * 50;
		else
			newHeight -= Input.GetAxis("Vertical2");

		if (newHeight < 15)
			newHeight = 15;

		if (newHeight > 60)
			newHeight = 60;

		height += (newHeight - height) * 8 * Time.deltaTime;
	}
}