using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public boss_death death;
	public Animator m_amiator;
	// Start is called before the first frame update
	public GameObject bulletPrefab;
	public DoorHandler door;
	public Transform player;
	public float maxHealth = 30.0f;
	private float health;
	public float direction = 1;
	public bool isFlipped = true;
	public bool playerdetected = true;
	float fireRate;
	float nextFire;
	public bool hit = true;

	public bool play = false;


	public bool invincible = true;
	// Distance from the player for the enemy to change state
	public float stateDist = 35.0f;

	private void Start()
    {
		m_amiator = this.GetComponent<Animator>();
		//transform.Rotate(0f, 180f, 0f);
		fireRate = 1f;
		nextFire = Time.time;
		invincible = true;
		health = maxHealth;
		m_amiator.SetBool("shooting", false);
	}
    private void Update()
    {
		if (invincible && Vector2.Distance(player.GetComponent<Rigidbody2D>().position, this.transform.position) <= stateDist)
		{
			invincible = false;
	
		}
		else if (!invincible)
		{ 
		
			CheckIfTimeToFire();
		}
	}
	public void CheckIfTimeToFire()
	{

		if (Time.time > nextFire)
		{
			Instantiate(bulletPrefab,transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
			m_amiator.SetBool("shooting", true);
		}
		m_amiator.SetBool("shooting", true);
	}

	public void damage(float t_damage)
	{
		if(!invincible)
        {
			health -= t_damage;
		}
		
			if (health <= 0.0f)
			{
				death.death_start(true);
				Destroy(this.gameObject);
			}
	}

	public float getHealth()
	{
		return health;
	}
	public void LookAtPlayer()
	{
		
	
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x < player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			direction = flipped.z;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			direction = flipped.z;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if(hit)
            {
                if (!collision.gameObject.GetComponent<PlayerController>().getIsInvincible())
                {
                    collision.gameObject.GetComponent<PlayerController>().decreseHealth(1, transform.position);
					AnalyticsManager.instance.data.enemyDamage[3]++;

					if (collision.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                    {
                      AnalyticsManager.instance.data.killedBy = "Boss";
                    }
                }
            }
        } 

    }
}