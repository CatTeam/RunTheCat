using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
	private delegate void GUIMethod();
	private GUIMethod currentGUIMethod;

	void Start()
	{
		currentGUIMethod = Game;
	}

	void OnGUI()
	{
		this.currentGUIMethod();
	}

	void Game()
	{

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Time.timeScale = 0;
			Debug.Log("Time scale changed to 0. Actual: " + Time.timeScale.ToString());
			currentGUIMethod = PauseMenu;	
		}
	}

	void PauseMenu()
	{

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Time.timeScale = 1;
			Debug.Log("Time scale changed to 1. Actual: " + Time.timeScale.ToString());
			currentGUIMethod = Game;	
		}
	}

	void ProgressLost()
	{

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			currentGUIMethod = PauseMenu;	
		}
	}

	void FailedLevel()
	{

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Application.LoadLevel(0);
		}
	}
}
