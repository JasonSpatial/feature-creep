using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class HeroMover : MonoBehaviour
{
	public int life;
	public float speed = 1.0f;
	public float rateOfFire = 0.5f;
	public GameObject projectile;
	public GameObject muzzleFlash;
	public GameObject hurtSound;
	public GameObject fireSound;
	public GameObject silverPowerUpSound;
	public GameObject goldPowerUpSound;
	public GameObject[] lifeBars;
	public GameObject[] deathBars;
	
	private Rigidbody rb;
	private float fireDelay;
	private Color originalColor;
	private SpriteRenderer sprite;
	

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		originalColor = sprite.color;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1") && Time.time > fireDelay)
		{
			Fire();
		}
		
		Vector3 rotations = new Vector3(Input.GetAxis("RightH"),0, Input.GetAxis("RightV"));
		if (rotations != Vector3.zero)
		{
			if (Time.time > fireDelay)
			{
				Fire();
			}
			rb.transform.rotation = Quaternion.LookRotation(rotations);
		}
	}

	void Fire()
	{
		fireDelay = Time.time + rateOfFire;
		GameObject fireClone = Instantiate(fireSound, transform.position, Quaternion.identity);
		GameObject flash = Instantiate(muzzleFlash, transform.position, transform.rotation);
		GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
		Destroy(fireClone, 1f);
		Destroy(flash, 0.2f);
		Destroy(clone, 5f);
	}
	
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));


		
		rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Feature"))
		{
			GameObject hurt = Instantiate(hurtSound, transform.position, Quaternion.identity);
			TakeDamage(other.gameObject.GetComponent<Enemy>().harm);
			Destroy(hurt, 1f);
		}

		if (other.CompareTag("PowerUp"))
		{
			print("powerup: " + other.name);
			if (other.name.StartsWith("GoldStar"))
			{
				GameObject goldSound = Instantiate(goldPowerUpSound, transform.position, Quaternion.identity);
				Destroy(goldSound, 1f);
				Destroy(other.gameObject);
				StartCoroutine(PowerUp(0.05f, 3));
			} else if (other.name.StartsWith("SilverBolt"))
			{
				GameObject silverSound = Instantiate(silverPowerUpSound, transform.position, Quaternion.identity);
				Destroy(silverSound, 1f);				
				Destroy(other.gameObject);
				StartCoroutine(PowerUp(0.09f, 3));
			}
		}
	}

	IEnumerator PowerUp(float fireRateIncrease, int forSeconds)
	{
		float originalRateOfFire = rateOfFire;
		rateOfFire = fireRateIncrease;
		yield return new WaitForSeconds(forSeconds);
		rateOfFire = originalRateOfFire;
	}

	IEnumerator ShowDamage()
	{
		
		sprite.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		sprite.color = originalColor;
	}
	
	void TakeDamage(int damage)
	{

		StartCoroutine(ShowDamage());	
		
		life -= damage;

		if (life < 3)
		{
			foreach (var lifeBar in lifeBars)
			{
				lifeBar.gameObject.SetActive(false);
			}

			if (life < 2)
			{
				print("life: " + life);
				deathBars[life].SetActive(false);
			}
			else
			{
				foreach (var deathBar in deathBars)
				{
					deathBar.gameObject.SetActive(true);
				}
			}
		} 
		else
		{
			lifeBars[life].gameObject.SetActive(false);
		}
		
		if (life <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
		GameManager.Instance.Lose();
	}
}








