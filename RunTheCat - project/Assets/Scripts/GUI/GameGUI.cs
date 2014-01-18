using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
	private const int MENU_HEIGHT = 225 * 2;
	private const int MENU_WIDTH = 210 * 2;
	private const int BUTTON_HEIGHT = 40 * 2;
	private const int BUTTON_WIDTH = 160 * 2;
	private const int HORIZONTAL_BREAK = 20 * 2;
	private const int MARIGIN = 25 * 2;
	private const int LABEL_HEIGHT = 25 * 2;

	private delegate void GUIMethod();
	private GUIMethod currentGUIMethod;

	void Start()
	{
		currentGUIMethod = Game;
		Time.timeScale = 1;
	}

	void OnGUI()
	{
		this.currentGUIMethod();
	}

	#region GUIs
	void Game()
	{	
		// TODO: get score from player object
		GUI.Box (new Rect (0,0,100,50), "Score: xxx");

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Pause();
		}

		// DEBUG: press F or Menu => level failed
		if (((Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Menu)) && Event.current.type == EventType.KeyUp)
		    || Player.instance.isLevelFailed)
		{
			Pause();
			currentGUIMethod = FailedLevel;
		}
	}

	void PauseMenu()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Pause");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "Sound ON/OFF"))
		{
			// TODO: toggle sound
			Debug.Log("TODO: toggle sound");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Main Menu"))
		{
			currentGUIMethod = ProgressLost;
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Back to game"))
		{
			Unpause();
		}		

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Unpause();
		}
	}

	void ProgressLost()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Are you sure? Your progress will be lost.");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Main menu"))
		{
			Application.LoadLevel("MainMenu");
		}

		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + BUTTON_WIDTH / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Back"))
		{
			currentGUIMethod = PauseMenu;
		}

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			currentGUIMethod = PauseMenu;	
		}
	}

	void FailedLevel()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "You failed.");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Main menu"))
		{
			Application.LoadLevel("MainMenu");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + BUTTON_WIDTH / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Retry"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}


		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			Application.LoadLevel("MainMenu");
		}
	}
	#endregion GUIs

	void Pause()
	{
		Time.timeScale = 0;
		currentGUIMethod = PauseMenu;
	}

	void Unpause()
	{
		Time.timeScale = 1;
		currentGUIMethod = Game;
	}
}
