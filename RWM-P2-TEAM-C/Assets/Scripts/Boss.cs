using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	// Start is called before the first frame update
	public Transform player;
	public BulletManager _bulletManager;
	public int direction = -1;
	public bool isFlipped = false;
	public bool playerdetected = false;
	 float _timeBetweenShots = 0.5f;

	private void Update()
    {
		LookAtPlayer();
		
	}
    public void LookAtPlayer()
	{
		Vector3 temp = transform.localScale;
		if (temp.x < 0) { direction = 1; }
		if (temp.x > 0) { direction = -1; }

		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			playerdetected = true;
			Debug.Log("player detected");
			shootPlayer();
			//collision.gameObject.GetComponent<PlayerController>().decreseHealth(10);


		}

	}

	void shootPlayer()
    {
		if(playerdetected && _bulletManager.canFire())
		 {
			Debug.Log("player SHOOT");
			_bulletManager.shootBullet();
			StartCoroutine("shootingCooldown");
		}
    }

	IEnumerator shootingCooldown()
	{
		yield return new WaitForSeconds(_timeBetweenShots);
		//_isShooting = false;
		//idleshoot = false;
		//_2dMovement.setStopMovement(false);
		yield return new WaitForSeconds(_timeBetweenShots);
		//_animator.SetBool("isShooting", _isShooting);
	}
}
