using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_healthbar : MonoBehaviour
{
	public float unitHeight = 10;
	public float size = 1;
	public float alpha = 1;

	public int lives;

	bool disabled = false;

	public GameObject healthBar;
	Image frame;
	Image inside;
	Image image;
	Transform canvas;
	Scrollbar bar;
	Text text;

	camera theCamera;
	float cameraOffset;

	Transform unit;

	public float currentHealth;
	public float maxHealth;

	void Start ()
	{
		unit = GetComponentInParent<Transform>();
		theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();

		GameObject healthBarObject = Instantiate(healthBar, unit.position, Quaternion.identity) as GameObject;

		canvas = healthBarObject.transform.FindChild("HealthBar");
		frame = healthBarObject.transform.FindChild("HealthBar").transform.FindChild("Frame").GetComponent<Image>();
		inside = healthBarObject.transform.FindChild("HealthBar").transform.FindChild("Inside").GetComponent<Image>();
		image = healthBarObject.transform.FindChild("HealthBar").transform.FindChild("Image").GetComponent<Image>();
		bar = healthBarObject.transform.FindChild("HealthBar").GetComponent<Scrollbar>();
		text = healthBarObject.transform.FindChild("HealthBar").FindChild("Text").GetComponent<Text>();
	}

	public void SetMaxHealth(float mH)
	{
		maxHealth = mH;
		currentHealth = mH;
	}

	public void UpdateBar (float health)
	{
		if (!disabled)
		{
			currentHealth = health;
			bar.size = health / maxHealth;
			text.text = health + "/" + maxHealth;
		}
	}

	public void RemoveBar()
	{
		disabled = true;
		Destroy(bar.gameObject);
	}

	void LateUpdate ()
	{
		if (!disabled)
		{
			canvas.transform.localScale = (new Vector3 (size,size,1));
			frame.canvasRenderer.SetColor(new Color (1, 1, 1, alpha + 1) );
			inside.canvasRenderer.SetColor(new Color (1, 1, 1, alpha + 2));
			image.canvasRenderer.SetColor(new Color (image.canvasRenderer.GetColor().r, image.canvasRenderer.GetColor().g, image.canvasRenderer.GetColor().b, alpha + 2));
			text.canvasRenderer.SetColor(new Color (1, 1, 1, alpha));

			bar.transform.position = new Vector3(transform.position.x, transform.position.y + theCamera.height * 0.05f + unitHeight, transform.position.z + theCamera.height * 0.04f);
			bar.transform.LookAt(new Vector3(theCamera.transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), -Vector3.up);
			bar.transform.rotation = Quaternion.Euler(bar.transform.rotation.eulerAngles.x + 180, 180, bar.transform.rotation.eulerAngles.z);
		}
	}
}
