using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_game : MonoBehaviour
{
	ui_healthbar playerBar;

	Scrollbar bar;
	Text text;
	Text livesText;

	float currentHealth = 100;
	float maxHealth = 100;

	bool pauseMenuVisible = false;
	bool settingsMenuVisible = false;
	bool creditsMenuVisible = false;
	bool deathScreenVisible = false;

	public bool isPaused = false;

	public float fov = 80;

	//camera theCamera;
	p_movement p_move;

	public GameObject pauseMenu;
	public GameObject settingsMenu;
	public GameObject creditsMenu;
	public GameObject deathScreen;

	int healthBarOption = 0;

	void Start ()
	{
		playerBar = GameObject.FindGameObjectWithTag("Player").GetComponent<ui_healthbar>();
		p_move = GameObject.FindGameObjectWithTag("Player").GetComponent<p_movement>();
		//theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();


		bar = transform.FindChild("HealthBar").GetComponent<Scrollbar>();
		text = transform.FindChild("HealthBar").FindChild("Text").GetComponent<Text>();
		livesText = transform.FindChild("HealthBar").FindChild("LivesText").GetComponent<Text>();
	}

	void Update ()
	{
		UpdateHealth();

		livesText.text = "x" + playerBar.lives;

		if (p_move.deadForReal)
		{
			deathScreenVisible = true;
		}

		if (healthBarOption == 0)
			playerBar.alpha = 1;
		else if (healthBarOption == 1)
			playerBar.alpha = 0;
		else if (healthBarOption == 2)
			playerBar.alpha = -1;
		else if (healthBarOption == 3)
			playerBar.alpha = -2;

		if (isPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

		if (pauseMenuVisible && !settingsMenuVisible && !creditsMenuVisible)
			pauseMenu.SetActive(true);
		else
			pauseMenu.SetActive(false);

		if (settingsMenuVisible)
			settingsMenu.SetActive(true);
		else
			settingsMenu.SetActive(false);

		if (creditsMenuVisible)
			creditsMenu.SetActive(true);
		else
			creditsMenu.SetActive(false);

		if (deathScreenVisible)
			deathScreen.SetActive(true);
		else
			deathScreen.SetActive(false);

		if (Input.GetButtonDown("Cancel") && !settingsMenuVisible && !creditsMenuVisible)
			TogglePauseMenu();
		else if (Input.GetButtonDown("Cancel") && settingsMenuVisible)
			ToggleSettingsMenu();
		else if (Input.GetButtonDown("Cancel") && creditsMenuVisible)
			ToggleCreditsMenu();
	}

	public void SetFov()
	{
		fov = settingsMenu.transform.FindChild("fovSlider").GetComponent<Slider>().value;
		Text fovnumber = settingsMenu.transform.FindChild("fovSlider").FindChild("fovNumber").GetComponent<Text>();
		fovnumber.text = "" + Mathf.Floor(fov);
		//theCamera.fov = fov;
	}

	public void TogglePauseMenu()
	{
		pauseMenuVisible = !pauseMenuVisible;
		isPaused =!isPaused;
	}

	public void ToggleSettingsMenu()
	{
		settingsMenuVisible = !settingsMenuVisible;
	}

	public void ToggleCreditsMenu()
	{
		creditsMenuVisible = !creditsMenuVisible;
	}

	public void ToggleMouseControls()
	{
		//p_move.mouseMovement = !p_move.mouseMovement;
		//theCamera.mouseZoom = !theCamera.mouseZoom;
		//Debug.Log(p_move.mouseMovement + " " + theCamera.mouseZoom);
	}

	public void ToggleHealthBar(int index)
	{
		if (index == 0 && healthBarOption != 0)
			healthBarOption = 0;
		else if (index == 1 && healthBarOption != 1)
			healthBarOption = 1;
		else if (index == 2 && healthBarOption != 2)
			healthBarOption = 2;
		else if (index == 3 && healthBarOption != 3)
			healthBarOption = 3;
	}

	public void ReloadGame()
	{
		Application.LoadLevel(0);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	void UpdateHealth()
	{
		currentHealth = playerBar.currentHealth;
		maxHealth = playerBar.maxHealth;
		
		bar.size = currentHealth / maxHealth;
		text.text = currentHealth + "/" + maxHealth;
	}
}