using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
	public AudioClip buttonSound;
	public AudioClip backgroundMusic;

	private AudioSource backgroundMusicSource;

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
		
		backgroundMusicSource = gameObject.AddComponent<AudioSource>();
		backgroundMusicSource.clip = backgroundMusic;
		backgroundMusicSource.loop = true;
		backgroundMusicSource.volume = 0.1f;
		backgroundMusicSource.Play();
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
			audio.PlayOneShot(buttonSound);
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
			audio.PlayOneShot(buttonSound);
			if (AudioListener.volume != 0)
			{
				AudioListener.volume = 0;
			}
			else
			{
				AudioListener.volume = 1;
			}
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Main Menu"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = ProgressLost;
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Back to game"))
		{
			audio.PlayOneShot(buttonSound);
			Unpause();
		}		

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			audio.PlayOneShot(buttonSound);
			Unpause();
		}
	}

	void ProgressLost()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Are you sure? Your progress will be lost.");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Main menu"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel("MainMenu");
		}

		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + BUTTON_WIDTH / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Back"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = PauseMenu;
		}

		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = PauseMenu;	
		}
	}

	void FailedLevel()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "You failed.");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Main menu"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel("MainMenu");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + BUTTON_WIDTH / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), (BUTTON_WIDTH - MARIGIN) / 2, BUTTON_HEIGHT), "Retry"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel(Application.loadedLevel);
		}


		if (Input.GetKeyUp (KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			audio.PlayOneShot(buttonSound);
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
