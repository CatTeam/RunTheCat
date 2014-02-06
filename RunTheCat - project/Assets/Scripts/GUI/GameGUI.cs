using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
    public GUIStyle passedLevelBackground;
    private GUIStyle next;
    private GUIStyle menu;

    public Texture buttonTexture;

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

    private const int NEXT_BTN_WIDTH = 194;
    private const int NEXT_BTN_HEIGHT = 70;
    private const int MENU_BTN_WIDTH = 202;
    private const int MENU_BTN_HEIGHT = 70;

	private delegate void GUIMethod();
	private GUIMethod currentGUIMethod;

    private int levelHighScore;

    private void LoadTextTextures()
    {
        next = new GUIStyle();
        next.fixedHeight = BUTTON_HEIGHT;
        next.fixedWidth = NEXT_BTN_WIDTH * BUTTON_HEIGHT / NEXT_BTN_HEIGHT;
        next.normal.background = Resources.Load("Text/" + "next") as Texture2D;
        next.hover.background = Resources.Load("Text" + "nextclicked") as Texture2D;

        menu = new GUIStyle();
        menu.fixedHeight = BUTTON_HEIGHT;
        menu.fixedWidth = MENU_BTN_WIDTH * BUTTON_HEIGHT / MENU_BTN_HEIGHT;
        menu.normal.background = Resources.Load("Text/" + "menu") as Texture2D;
        menu.hover.background = Resources.Load("Text/" + "menuclick") as Texture2D;
        menu.margin.left =0;

        next.margin.left = (int)(menu.fixedWidth - next.fixedWidth) / 2;

        passedLevelBackground = new GUIStyle();
        passedLevelBackground.normal.background = Resources.Load("Backgrounds/" + "passed_level_background") as Texture2D;
        passedLevelBackground.stretchHeight = true;
        passedLevelBackground.stretchWidth = true;
    }
	void Start()
	{
        LoadTextTextures();
        levelHighScore = PlayerPrefsHelper.GetLevelHighScore(GameObject.FindObjectOfType<FinishingLine>().levelNo);
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
		GUI.Box (new Rect (0,0,100,50), "Highscore: " + levelHighScore + "\nScore: " + Player.instance.Score.ToString());

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

        if(Player.instance.isLevelCompleted)
        {
			Pause();
            currentGUIMethod = PassedLevel;
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
                PlayerPrefsHelper.SaveMusicOn(false);
			}
			else
			{
				AudioListener.volume = 1;
                PlayerPrefsHelper.SaveMusicOn(true);
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

    void PassedLevel()
    {
        //GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Congrats!");
        GUILayout.Label("",passedLevelBackground, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect((Screen.width - menu.fixedWidth)/2, (Screen.height) / 2 - BUTTON_HEIGHT, Screen.width, Screen.height));
        {
            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("", next))
                {
                    audio.PlayOneShot(buttonSound);
                    if(Application.loadedLevel + 1 == Application.levelCount)
                        Application.LoadLevel("MainMenu");
                    else
                        Application.LoadLevel(Application.loadedLevel+1);
                }

                if (GUILayout.Button("", menu))
                {
                    audio.PlayOneShot(buttonSound);
                    Application.LoadLevel("MainMenu");
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
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
