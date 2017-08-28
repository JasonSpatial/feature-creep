using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{


	public int life;
	public int harm;
	public GameObject powerUp;
	public GameObject hitSound;

	public float speed = 3;

	public float rotationSpeed = 3;

	private Transform target;
	private int powerUpTarget;

	void Start ()
	{
		powerUpTarget = Random.Range(1, 15);
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate()
	{
		if (target != null)
		{
			Vector3 randomization = new Vector3(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
			transform.rotation =
				Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position + randomization), rotationSpeed * Time.fixedDeltaTime);

			transform.position += transform.forward * speed * Time.deltaTime;
		}
	}

	public void TakeDamage(int damage = 1)
	{
		life -= damage;
		
		GameObject hitClone = Instantiate(hitSound, transform.position, Quaternion.identity);
		Destroy(hitClone, 1f);

		if (life <= 0)
		{
			if (Random.Range(1, 15) == powerUpTarget)
			{
				Instantiate(powerUp, transform.position, Quaternion.identity);
			}
			
			GameManager.Instance.IncreaseScore();
			Destroy(gameObject);
		}
		else if(life == 1)
		{
			speed *= 1.2f;
		}
	}
}
