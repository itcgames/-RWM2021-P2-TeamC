using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
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

	private void Start()
    {
		transform.Rotate(0f, 180f, 0f);
		fireRate = 1f;
		nextFire = Time.time;
		health = maxHealth;
	}
    private void Update()
    {
		
		
		if(door.playerthrough)
        {
			CheckIfTimeToFire();
		}
		LookAtPlayer();
	
		
	}
	public void CheckIfTimeToFire()
	{
		if (Time.time > nextFire)
		{
			Instantiate(bulletPrefab,transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
		}

	}

	public void damage(float t_damage)
	{

			health -= t_damage;
			if (health <= 0.0f)
			{
				Destroy(this.gameObject);
			}
		Debug.Log("Health:" + health);
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
			Debug.Log(direction);
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			direction = flipped.z;
			Debug.Log(direction);
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
				collision.gameObject.GetComponent<PlayerController>().decreseHealth(1,transform.position);
				Debug.Log("player detected");
			}
		
			
		}        
	}
}
