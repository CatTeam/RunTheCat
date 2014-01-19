using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{
	public AudioClip buttonSound;
	public AudioClip backgroundMusic;

	private AudioSource backgroundMusicSource;

	private const int MENU_HEIGHT = 265 * 2;
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
		currentGUIMethod = MainMenu;
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
	
	void MainMenu()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Main Menu");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "Play"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = Levels;
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Sound ON/OFF"))
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
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Credits"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = Credits;
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 3 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Exit"))
		{
			Application.Quit();
		 	Debug.Log("Quit");
		}		
		FunctionalKeys();
	}
	
	void Levels()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Main Menu");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "Test level"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel("firstscene");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Level 2"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel("Level2");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Level 3"))
		{
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel("Level3");
		}
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 3 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Back"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = MainMenu;
		}		
		FunctionalKeys();
	}
	
	void Credits()
	{
		GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Credits");
		
		GUI.Label(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN, BUTTON_WIDTH, LABEL_HEIGHT), "Harriet");
		
		GUI.Label(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (LABEL_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, LABEL_HEIGHT), "Iroq");
		
		GUI.Label(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (LABEL_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, LABEL_HEIGHT), "Kostka");
		
		GUI.Label(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 3 * (LABEL_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, LABEL_HEIGHT), "Nahikka");
		
		if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 4 * (LABEL_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Back"))
		{
			audio.PlayOneShot(buttonSound);
			currentGUIMethod = MainMenu;
			return;
		}
		
		FunctionalKeys();
	}
	
	void FunctionalKeys()
	{
		if (Input.GetKeyUp(KeyCode.Escape) && Event.current.type == EventType.KeyUp)
		{
			audio.PlayOneShot(buttonSound);
			if (currentGUIMethod == MainMenu)
			{
				Application.Quit();
				Debug.Log("Quit");
			}
			else
			{
				currentGUIMethod = MainMenu;
			}
		}
	}
}

