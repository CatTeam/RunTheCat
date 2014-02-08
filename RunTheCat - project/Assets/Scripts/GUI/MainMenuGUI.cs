using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{
    //public GUIStyle back;


    public GUIStyle LevelText;
    public int LevelsCount = 6;
    public int levelsInRow = 4;

    public AudioClip buttonSound;
    public AudioClip backgroundMusic;

    private GUIStyle mainMenuBackground;
    private GUIStyle play;
    private GUIStyle sound;
    private GUIStyle credits;
    private GUIStyle exit;
    private GUIStyle frame;
    private GUIStyle menu;
    private GUIStyle lvlButton;
    private GUIStyle credits_label;

    private AudioSource backgroundMusicSource;

    private readonly int CREDITS_FONT_SIZE = 32 *Screen.height / 800;
    private const int MENU_HEIGHT = 265 * 2;
    private const int MENU_WIDTH = 210 * 2;
    private readonly int BUTTON_HEIGHT= Screen.height / 10;
    private const int BUTTON_WIDTH = 160 * 2;
    private const int HORIZONTAL_BREAK = 20 * 2;
    private const int MARGIN = 25 * 2;
    private const int LABEL_HEIGHT = 25 * 2;
    private int LEVEL_HEIGHT = Screen.height / 8;

    private const int MENU_BTN_WIDTH = 202;
    private const int MENU_BTN_HEIGHT = 70;
    private const int LVL_BTN_WIDTH = 116;
    private const int LVL_BTN_HEIGHT = 137;
    private const int PLAY_BTN_WIDTH = 189;
    private const int PLAY_BTN_HEIGHT = 71;
    private const int SOUND_BTN_WIDTH = 519;
    private const int SOUND_BTN_HEIGHT = 74;
    private const int CREDITS_BTN_WIDTH = 307;
    private const int CREDITS_BTN_HEIGHT = 71;
    private const int EXIT_BTN_WIDTH = 162;
    private const int EXIT_BTN_HEIGHT = 69;

    private delegate void GUIMethod();
    private GUIMethod currentGUIMethod;

    void Awake()
    {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = 0.1f;
        LoadTextures();
    }
    void Start()
    {
        LoadTextures();
        currentGUIMethod = MainMenu;
        Time.timeScale = 1;

        backgroundMusicSource.Play();

        AudioListener.volume = PlayerPrefsHelper.GetMusicOn() ? 1 : 0;
    }

    private void LoadTextures()
    {
        menu = new GUIStyle();
        menu.fixedHeight = BUTTON_HEIGHT / 2;
        menu.fixedWidth = MENU_BTN_WIDTH * BUTTON_HEIGHT / 2 / MENU_BTN_HEIGHT;
        menu.normal.background = Resources.Load("Text/" + "menu") as Texture2D;
        menu.hover.background = Resources.Load("Text/" + "menuclick") as Texture2D;
        menu.margin.left = (int)(Screen.width - menu.fixedWidth) / 2;

        mainMenuBackground = new GUIStyle();
        play = new GUIStyle();
        sound = new GUIStyle();
        credits = new GUIStyle();
        credits_label = new GUIStyle();
        exit = new GUIStyle();
        frame = new GUIStyle();

        mainMenuBackground.normal.background = Resources.Load("Backgrounds/menutlo") as Texture2D;
        frame.normal.background = Resources.Load("Backgrounds/big_frame") as Texture2D;

        play.fixedHeight = BUTTON_HEIGHT / 2;
        play.fixedWidth = PLAY_BTN_WIDTH * BUTTON_HEIGHT / 2 / PLAY_BTN_HEIGHT;
        play.normal.background = Resources.Load("Text/" + "play") as Texture2D;
        play.hover.background = Resources.Load("Text/" + "playclick") as Texture2D;
        play.margin.left = MARGIN;

        sound.fixedHeight = BUTTON_HEIGHT / 2;
        sound.fixedWidth = SOUND_BTN_WIDTH * BUTTON_HEIGHT / 2 / SOUND_BTN_HEIGHT;
        sound.normal.background = Resources.Load("Text/" + "musiconoff") as Texture2D;
        sound.hover.background = Resources.Load("Text/" + "musiconoffclick") as Texture2D;
        sound.margin.left = -MARGIN;

        credits.fixedHeight = BUTTON_HEIGHT / 2;
        credits.fixedWidth = CREDITS_BTN_WIDTH * BUTTON_HEIGHT / 2 / CREDITS_BTN_HEIGHT;
        credits.normal.background = Resources.Load("Text/" + "credits") as Texture2D;
        credits.hover.background = Resources.Load("Text/" + "creditsclick") as Texture2D;

        credits_label.fixedHeight = BUTTON_HEIGHT;
        credits_label.fixedWidth = CREDITS_BTN_WIDTH * BUTTON_HEIGHT/ CREDITS_BTN_HEIGHT;
        credits_label.normal.background = Resources.Load("Text/" + "credits") as Texture2D;
        credits_label.hover.background = Resources.Load("Text/" + "creditsclick") as Texture2D;

        exit.fixedHeight = BUTTON_HEIGHT / 2;
        exit.fixedWidth = EXIT_BTN_WIDTH * BUTTON_HEIGHT / 2 / EXIT_BTN_HEIGHT;
        exit.normal.background = Resources.Load("Text/" + "exit") as Texture2D;
        exit.hover.background = Resources.Load("Text/" + "exitclick") as Texture2D;
        exit.margin.left = -MARGIN / 2;

        lvlButton = new GUIStyle();
        lvlButton.fixedHeight = LEVEL_HEIGHT;
        lvlButton.fixedWidth = LVL_BTN_WIDTH * LEVEL_HEIGHT / LVL_BTN_HEIGHT;
    }

    void OnGUI()
    {
        this.currentGUIMethod();
    }

    void MainMenu()
    {
        GUILayout.Label("", mainMenuBackground, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect(Screen.height / 8 - MARGIN /2 , Screen.height / 2 , Screen.width, Screen.height / 4));
        {
            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("", play))
                {
                    audio.PlayOneShot(buttonSound);
                    currentGUIMethod = Levels;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", sound))
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
                if (GUILayout.Button("", credits))
                {
                    audio.PlayOneShot(buttonSound);
                    currentGUIMethod = Credits;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("", exit))
                {
                    Application.Quit();
                    Debug.Log("Quit");
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        FunctionalKeys();
    }

    void Levels()
    {
        GUILayout.Label("", frame, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));

        GUILayout.BeginArea(new Rect(MARGIN, MARGIN, Screen.width - MARGIN * 2, Screen.height - MARGIN));
        {
            GUILayout.Label("", LevelText, GUILayout.Width(Screen.width), GUILayout.Height(30));
            GUILayout.BeginVertical(GUILayout.Height(Mathf.Ceil(LevelsCount / levelsInRow) * (lvlButton.fixedHeight + MARGIN/2)), GUILayout.Width(Screen.width));
            {
                GUILayout.BeginHorizontal(GUILayout.Width(Screen.width - MARGIN * 2));
                for (int i = 1; i <= LevelsCount; i++)
                {
                    if (i > PlayerPrefsHelper.GetFinishedLevelsNo())
                    {
                        lvlButton.normal.background = Resources.Load("LevelThumbs/" + i) as Texture2D;
                        lvlButton.hover.background = Resources.Load("LevelThumbs/" + i + "c") as Texture2D;
                    }
                    else
                    {
                        lvlButton.normal.background = lvlButton.hover.background = Resources.Load("LevelThumbs/v") as Texture2D;
                    }

                    if (i % (levelsInRow + 1) == 0)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                        GUILayout.BeginHorizontal(GUILayout.Width(Screen.width - MARGIN * 2));
                    }
                    if (GUILayout.Button("", lvlButton))
                    {
                        audio.PlayOneShot(buttonSound);
                        if (PlayerPrefsHelper.GetFinishedLevelsNo() + 1 >= i)
                            Application.LoadLevel("Level" + i);
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        if (GUILayout.Button("", menu))
        {
            audio.PlayOneShot(buttonSound);
            currentGUIMethod = MainMenu;
        }
        GUILayout.EndArea();

        FunctionalKeys();
    }

    void Credits()
    {
        var credits_text_style = new GUIStyle();
        credits_text_style.fontSize = CREDITS_FONT_SIZE;
        GUI.contentColor = Color.black;
        GUILayout.Label("", frame, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUILayout.BeginArea(new Rect((Screen.width - credits_label.fixedWidth) / 2, MARGIN * 2, Screen.width - (Screen.width - credits_label.fixedWidth) / 2, Screen.height / 6));
        {
            GUILayout.Label("", credits_label);
        }
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(MARGIN / 2, MARGIN * 3 + credits_label.fixedHeight, Screen.width - MARGIN, Screen.height / 2));
        {
            GUILayout.Label("Programming", credits_text_style);
            GUILayout.Label("");
            GUILayout.Label("Michał \"iroq\" Szewczak", credits_text_style);
            GUILayout.Label("Emilia \"Harriet\" Szymańska", credits_text_style);
            GUILayout.Label("Dominika \"Nahikka\" Bodzon", credits_text_style);
            GUILayout.Label("");
            GUILayout.Label("Graphics", credits_text_style);
            GUILayout.Label("");
            GUILayout.Label("Katarzyna \"Kostka\" Garstka", credits_text_style);
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect((Screen.width - menu.fixedWidth) / 2, Screen.height/2 + Screen.height/6 + MARGIN * 3, Screen.width - (Screen.width - menu.fixedWidth) / 2, Screen.height / 6));
        {
            if (GUILayout.Button("", menu))
            {
                audio.PlayOneShot(buttonSound);
                currentGUIMethod = MainMenu;
                return;
            }
        }
        GUILayout.EndArea();

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

