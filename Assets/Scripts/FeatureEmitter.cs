using UnityEngine;

public class FeatureEmitter : MonoBehaviour
{

	public GameObject[] features;

	public float maxEmissionRate;
	public float baseEmissionRate;

	private float emissionDelay;

	private float lastEmissionTime;

	private int featureCount;

	void Start ()
	{
		emissionDelay = 5;
		featureCount = features.Length;
	}
	
	void Update () {
		if (Time.timeSinceLevelLoad > emissionDelay && GameManager.Instance.gameStarted)
		{
			float emissionFactor = Random.Range(0, maxEmissionRate);
			emissionDelay = Time.timeSinceLevelLoad + baseEmissionRate + emissionFactor;
			
			if (featureCount != 0)
			{
				featureCount--;
				Instantiate(features[featureCount], transform.position, Quaternion.identity);
				
			}
			else
			{
//				Destroy(gameObject);
			}
		}	
	}
}
