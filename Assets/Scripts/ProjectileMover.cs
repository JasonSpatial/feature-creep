using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
	public float Speed = 2f;

	void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * Speed;
		
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Feature"))
		{
			other.gameObject.GetComponent<Enemy>().TakeDamage();
			Destroy(gameObject);
		}
	}
}
