using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

	public static MusicPlayer Instance = null;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
