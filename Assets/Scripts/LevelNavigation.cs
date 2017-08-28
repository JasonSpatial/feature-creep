using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavigation : MonoBehaviour
{

	private LevelManager controller;
	
	public Level currentLevel;
	
	Dictionary<string, Level> levels = new Dictionary<string, Level>();

	public void LoadLevel(string name)
	{
		currentLevel = levels[name];
		controller.LoadLevel();
	}

	private void Awake()
	{
		controller = GetComponent<LevelManager>();
	}
}
