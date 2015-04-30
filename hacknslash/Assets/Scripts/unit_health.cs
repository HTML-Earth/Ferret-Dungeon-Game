using UnityEngine;
using System.Collections;

public class unit_health : MonoBehaviour
{
	public bool invulnerable = false;
	public bool cursed = false;
	public float health = 100;
	public bool notPlayer = false;
	public int lives = 0;

	public bool regenerating;
	public float regen = 1;
	public float regenCooldown;

	public Vector3 spawnPosition;

	float maxHealth = 100;

	bool disabled = false;

	ui_healthbar healthbar;

	p_animation p_anim;
	p_movement p_move;
	p_combat p_comb;
	ai_skullspider ai_spider;

	void Start ()
	{
		regenCooldown = Time.time;
		spawnPosition = transform.position;

		maxHealth = health;
		healthbar = GetComponent<ui_healthbar>();

		healthbar.SetMaxHealth(maxHealth);

		if (notPlayer)
		{
			ai_spider = GetComponent<ai_skullspider>();
		}
		else
		{
			healthbar.lives = lives;
			p_anim = GetComponent<p_animation>();
			p_move = GetComponent<p_movement>();
			p_comb = GetComponent<p_combat>();
			InvokeRepeating("HealthRegen", 0, 0.3F);
		}

	}

	void Update ()
	{
		if ( Time.time-regenCooldown > 3 )
		{
			regenerating = true;
			regenCooldown = Time.time;
		}
	}

	public void Damage(float value)
	{
		if (!invulnerable)
		{
			if (!notPlayer)
			{
				regenerating = false;
				regenCooldown = Time.time;
			}

			if (health <= value)
			{
				health = 0;
				updateHealthBar();

				if (!disabled)
				{
					disabled = true;

					if (notPlayer)
					{
						ai_spider.dead = true;
						healthbar.RemoveBar();
					}
					else
					{
						if (lives > 0)
				        {
							lives--;
							healthbar.lives = lives;
						}

						StartCoroutine("Die");
					}
				}
			}
			else
			{
				health -= value;
				updateHealthBar();
			}
		}
	}

	public void Heal(float value)
	{
		if (!cursed)
		{
			health += value;

			if (health > maxHealth)
				health = maxHealth;

			updateHealthBar();
		}
	}

	public void HealthRegen ()
	{
		if (regenerating)
		{
			Heal(regen);
		}
	}

	IEnumerator Die()
	{
		p_move.disabled = true;
		p_comb.disabled = true;
		p_anim.dead = true;

		if (lives < 1)
		{
			p_move.deadForReal = true;
		}
		else
		{
			yield return new WaitForSeconds(3);

			p_move.disabled = false;
			p_comb.disabled = false;
			p_anim.dead = false;

			disabled = false;
			health = maxHealth;
			updateHealthBar();

			transform.position = spawnPosition;
		}
	}

	void updateHealthBar()
	{
		healthbar.UpdateBar(health);
	}
}
