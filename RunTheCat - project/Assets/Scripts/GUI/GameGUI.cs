using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
    private GUIStyle passedLevelBackground;
    private GUIStyle failedLevelBackground;
    private GUIStyle bigFrame;

    private GUIStyle popupBackground;
    private GUIStyle next;
    private GUIStyle menu;
    private GUIStyle level;
    private GUIStyle completed;
    private GUIStyle game;
    private GUIStyle over;
    private GUIStyle again;
    private GUIStyle yes;
    private GUIStyle no;
    private GUIStyle music;
    private GUIStyle resume;
    private GUIStyle menuInPause;
    private GUIStyle pause;

    public Texture buttonTexture;

    public AudioClip buttonSound;
    public AudioClip backgroundMusic;

    private AudioSource backgroundMusicSource;

    private const int MENU_HEIGHT = 225 * 2;
    private const int MENU_WIDTH = 210 * 2;
    private readonly int BUTTON_HEIGHT = Screen.height / 10;
    private const int BUTTON_WIDTH = 160 * 2;
    private const int HORIZONTAL_BREAK = 20 * 2;
    private const int MARGIN = 25 * 2;
    private const int LABEL_HEIGHT = 25 * 2;
    private int POPUP_WIDTH = 0;

    private const int NEXT_BTN_WIDTH = 194;
    private const int NEXT_BTN_HEIGHT = 70;
    private const int MENU_BTN_WIDTH = 202;
    private const int MENU_BTN_HEIGHT = 70;
    private const int LEVEL_LBL_WIDTH = 230;
    private const int LEVEL_LBL_HEIGHT = 71;
    private const int COMPL_LBL_WIDTH = 372;
    private const int COMPL_LBL_HEIGHT = 74;
    private const int GAME_LBL_WIDTH = 195;
    private const int GAME_LBL_HEIGHT = 71;
    private const int OVER_LBL_WIDTH = 191;
    private const int OVER_LBL_HEIGHT = 75;
    private const int AGAIN_BTN_WIDTH = 218;
    private const int AGAIN_BTN_HEIGHT = 73;
    private const int POPUP_BCG_HEIGHT = 340;
    private const int POPUP_BCG_WIDTH = 677;
    private const int YES_BTN_WIDTH = 142;
    private const int YES_BTN_HEIGHT = 71;
    private const int NO_BTN_WIDTH = 103;
    private const int NO_BTN_HEIGHT = 71;
    private const int MUSIC_BTN_WIDTH = 519;
    private const int MUSIC_BTN_HEIGHT = 74;
    private const int RESUME_BTN_WIDTH = 293;
    private const int RESUME_BTN_HEIGHT = 73;
    private const int PAUSE_BTN_WIDTH = 234;
    private const int PAUSE_BTN_HEIGHT = 72;

    private delegate void GUIMethod();
    private GUIMethod currentGUIMethod;

    private int levelHighScore;

    private void LoadTextTextures()
    {
        POPUP_WIDTH = Screen.width - 2 * MARGIN;
        next = new GUIStyle();
        next.fixedHeight = BUTTON_HEIGHT / 2;
        next.fixedWidth = NEXT_BTN_WIDTH * BUTTON_HEIGHT / 2 / NEXT_BTN_HEIGHT;
        next.normal.background = Resources.Load("Text/" + "next") as Texture2D;
        next.hover.background = Resources.Load("Text/" + "nextclick") as Texture2D;

        menu = new GUIStyle();
        menu.fixedHeight = BUTTON_HEIGHT / 2;
        menu.fixedWidth = MENU_BTN_WIDTH * BUTTON_HEIGHT / 2 / MENU_BTN_HEIGHT;
        menu.normal.background = Resources.Load("Text/" + "menu") as Texture2D;
        menu.hover.background = Resources.Load("Text/" + "menuclick") as Texture2D;

        passedLevelBackground = new GUIStyle();
        passedLevelBackground.normal.background = Resources.Load("Backgrounds/" + "passed_level_background") as Texture2D;
        passedLevelBackground.stretchHeight = true;
        passedLevelBackground.stretchWidth = true;

        level = new GUIStyle();
        level.fixedHeight = BUTTON_HEIGHT;
        level.fixedWidth = LEVEL_LBL_WIDTH * BUTTON_HEIGHT / LEVEL_LBL_HEIGHT;
        level.normal.background = Resources.Load("Text/" + "level") as Texture2D;

        completed = new GUIStyle();
        completed.fixedHeight = BUTTON_HEIGHT - 11;
        completed.fixedWidth = COMPL_LBL_WIDTH * completed.fixedHeight / COMPL_LBL_HEIGHT;
        completed.normal.background = Resources.Load("Text/" + "complete") as Texture2D;

        completed.margin.left = menu.margin.left = (int)(level.fixedWidth - completed.fixedWidth) / 2;

        game = new GUIStyle();
        game.fixedHeight = BUTTON_HEIGHT * 2 / 3;
        game.fixedWidth = GAME_LBL_WIDTH * game.fixedHeight / GAME_LBL_HEIGHT;
        game.normal.background = Resources.Load("Text/" + "game") as Texture2D;

        over = new GUIStyle();
        over.fixedHeight = BUTTON_HEIGHT;
        over.fixedWidth = OVER_LBL_WIDTH * BUTTON_HEIGHT / OVER_LBL_HEIGHT;
        over.normal.background = Resources.Load("Text/" + "over") as Texture2D;

        over.margin.left = menu.margin.left = (int)(game.fixedWidth - over.fixedWidth) / 2;

        again = new GUIStyle();
        again.fixedHeight = BUTTON_HEIGHT / 2;
        again.fixedWidth = AGAIN_BTN_WIDTH * BUTTON_HEIGHT / 2 / AGAIN_BTN_HEIGHT;
        again.normal.background = Resources.Load("Text/" + "again") as Texture2D;
        again.hover.background = Resources.Load("Text/" + "againclick") as Texture2D;

        popupBackground = new GUIStyle();
        popupBackground.fixedHeight = POPUP_BCG_HEIGHT * POPUP_WIDTH / POPUP_BCG_WIDTH;
        popupBackground.fixedWidth = POPUP_WIDTH;
        popupBackground.normal.background = Resources.Load("Backgrounds/" + "popup_background") as Texture2D;

        yes = new GUIStyle();
        yes.fixedHeight = BUTTON_HEIGHT / 2;
        yes.fixedWidth = YES_BTN_WIDTH * BUTTON_HEIGHT / 2 / YES_BTN_HEIGHT;
        yes.normal.background = Resources.Load("Text/" + "yes") as Texture2D;
        yes.hover.background = Resources.Load("Text/" + "yesclick") as Texture2D;

        no = new GUIStyle();
        no.fixedHeight = BUTTON_HEIGHT / 2;
        no.fixedWidth = NO_BTN_WIDTH * BUTTON_HEIGHT / 2 / NO_BTN_HEIGHT;
        no.normal.background = Resources.Load("Text/" + "no") as Texture2D;
        no.hover.background = Resources.Load("Text/" + "noclick") as Texture2D;

        music = new GUIStyle();
        music.fixedHeight = BUTTON_HEIGHT / 2;
        music.fixedWidth = MUSIC_BTN_WIDTH * BUTTON_HEIGHT / 2 / MUSIC_BTN_HEIGHT;
        music.normal.background = Resources.Load("Text/" + "musiconoff") as Texture2D;
        music.hover.background = Resources.Load("Text/" + "musiconoffclick") as Texture2D;

        resume = new GUIStyle();
        resume.fixedHeight = BUTTON_HEIGHT / 2;
        resume.fixedWidth = RESUME_BTN_WIDTH * BUTTON_HEIGHT / 2 / RESUME_BTN_HEIGHT;
        resume.normal.background = Resources.Load("Text/" + "resume") as Texture2D;
        resume.hover.background = Resources.Load("Text/" + "resume") as Texture2D;

        menuInPause = new GUIStyle();
        menuInPause.fixedHeight = menu.fixedHeight;
        menuInPause.fixedWidth = menu.fixedWidth;
        menuInPause.normal.background = menu.normal.background;
        menuInPause.hover.background = menu.hover.background;

        menuInPause.margin.left = (int)(music.fixedWidth - menuInPause.fixedWidth) / 2;
        resume.margin.left = (int)(music.fixedWidth - resume.fixedWidth) / 2;

        pause = new GUIStyle();
        pause.fixedHeight = BUTTON_HEIGHT;
        pause.fixedWidth = PAUSE_BTN_WIDTH * BUTTON_HEIGHT / PAUSE_BTN_HEIGHT;
        pause.normal.background = Resources.Load("Text/" + "pause") as Texture2D;

        passedLevelBackground = new GUIStyle();
        failedLevelBackground = new GUIStyle();
        bigFrame = new GUIStyle();

        passedLevelBackground.normal.background = Resources.Load("Backgrounds/passed_level_background") as Texture2D;
        failedLevelBackground.normal.background = Resources.Load("Backgrounds/failed_level_background") as Texture2D;
        bigFrame.normal.background = Resources.Load("Backgrounds/big_frame") as Texture2D;
    }

    void Awake()
    {
        LoadTextTextures();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = 0.1f;
    }
    void Start()
    {
        levelHighScore = PlayerPrefsHelper.GetLevelHighScore(GameObject.FindObjectOfType<FinishingLine>().levelNo);
        currentGUIMethod = Game;
        Time.timeScale = 1;

        backgroundMusicSource.Play();
    }

    void OnGUI()
    {
        this.currentGUIMethod();
    }

    #region GUIs
    void Game()
    {
        GUI.Box(new Rect(0, 0, 100, 50), "Highscore: " + levelHighScore + "\nScore: " + Player.instance.Score.ToString());

        if (Input.GetKeyUp(KeyCode.Escape) && Event.current.type == EventType.KeyUp)
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

        //if (Input.GetKeyUp(KeyCode.A) && Event.current.type == EventType.KeyUp)
        //{
        //    Pause();
        //    currentGUIMethod = PauseMenu;
        //}

        if (Player.instance.isLevelCompleted)
        {
            Pause();
            currentGUIMethod = PassedLevel;
        }
    }

    void PauseMenu()
    {
        GUILayout.Label("", bigFrame, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect((Screen.width - pause.fixedWidth) / 2, MARGIN * 2, Screen.width - (Screen.width - pause.fixedWidth) / 2, Screen.height / 6));
        {
            GUILayout.Label("", pause);
        }
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect((Screen.width - music.fixedWidth) / 2, Screen.height / 3, Screen.width - (Screen.width - music.fixedWidth) / 2, Screen.height / 4));
        {
            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("", music))
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
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", menuInPause))
                {
                    audio.PlayOneShot(buttonSound);
                    currentGUIMethod = ProgressLost;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", resume))
                {
                    audio.PlayOneShot(buttonSound);
                    Unpause();
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (Input.GetKeyUp(KeyCode.Escape) && Event.current.type == EventType.KeyUp)
        {
            audio.PlayOneShot(buttonSound);
            Unpause();
        }
    }

    void ProgressLost()
    {
        GUILayout.BeginArea(new Rect(MARGIN, Screen.height * 2 / 3, POPUP_WIDTH, popupBackground.fixedHeight));
        {
            GUILayout.Label("", popupBackground, GUILayout.Width(popupBackground.fixedWidth), GUILayout.Height(popupBackground.fixedHeight));
        }
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(MARGIN * 2, Screen.height * 2 / 3 + (int)(MARGIN * 2.5), POPUP_WIDTH - MARGIN * 2, popupBackground.fixedHeight));
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("", yes))
                {
                    audio.PlayOneShot(buttonSound);
                    Application.LoadLevel("MainMenu");
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", no))
                {
                    audio.PlayOneShot(buttonSound);
                    currentGUIMethod = PauseMenu;
                }
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        if (Input.GetKeyUp(KeyCode.Escape) && Event.current.type == EventType.KeyUp)
        {
            audio.PlayOneShot(buttonSound);
            currentGUIMethod = PauseMenu;
        }
    }

    void FailedLevel()
    {
        GUILayout.Label("", failedLevelBackground, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect((Screen.width - over.fixedWidth) / 2, MARGIN * 2, Screen.width - (Screen.width - game.fixedWidth) / 2, Screen.height / 3));
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("", game);
                GUILayout.Label("", over);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect((Screen.width - over.fixedWidth) / 4, Screen.height * 2 / 3 + MARGIN, Screen.width - (Screen.width - over.fixedWidth) / 2, Screen.height / 3));
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("", menu))
                {
                    audio.PlayOneShot(buttonSound);
                    Application.LoadLevel("MainMenu");
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", again))
                {
                    audio.PlayOneShot(buttonSound);
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        if (Input.GetKeyUp(KeyCode.Escape) && Event.current.type == EventType.KeyUp)
        {
            audio.PlayOneShot(buttonSound);
            Application.LoadLevel("MainMenu");
        }
    }

    void PassedLevel()
    {
        GUILayout.Label("", passedLevelBackground, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect((Screen.width - level.fixedWidth) / 4, MARGIN, Screen.width - (Screen.width - level.fixedWidth) / 2, Screen.height / 3));
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("", level);
                GUILayout.Label("", completed);
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("", menu))
                    {
                        audio.PlayOneShot(buttonSound);
                        Application.LoadLevel("MainMenu");
                    }
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("", next))
                    {
                        audio.PlayOneShot(buttonSound);
                        if (Application.loadedLevel + 1 == Application.levelCount)
                            Application.LoadLevel("MainMenu");
                        else
                            Application.LoadLevel(Application.loadedLevel + 1);
                    }
                }
                GUILayout.EndHorizontal();
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
