using UnityEngine;

public class FartExpeller : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
		if (transform.localScale.x < 0.1f)
		{
			Vector3 tempScale = transform.localScale;
			tempScale *= 1.05f;
			transform.localScale = tempScale;
		}
	}
}
