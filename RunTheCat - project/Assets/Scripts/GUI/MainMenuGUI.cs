using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{
    private const int MENU_HEIGHT = 265;
    private const int MENU_WIDTH = 210;
    private const int BUTTON_HEIGHT = 40;
    private const int BUTTON_WIDTH = 160;
    private const int HORIZONTAL_BREAK = 20;
    private const int MARIGIN = 25;
    private const int LABEL_HEIGHT = 25;

    private delegate void GUIMethod();
    private GUIMethod currentGUIMethod;

    void Start()
    {
        currentGUIMethod = MainMenu;
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
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Options"))
        {
            currentGUIMethod = Options;
        }

        if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Credits"))
        {
            currentGUIMethod = Credits;
        }

        if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 3 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Exit"))
        {
            Application.Quit();
        }
    }

    void Options()
    {
        GUI.Box(new Rect((Screen.width - MENU_WIDTH) / 2, (Screen.height - MENU_HEIGHT) / 2, MENU_WIDTH, MENU_HEIGHT), "Options");

        if (GUI.Toggle(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN, BUTTON_WIDTH, BUTTON_HEIGHT), false, "Sound"))
        {
            // TODO: turn off the music and the SFX
        }

        if (GUI.Toggle(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + 40, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH - 40, BUTTON_HEIGHT), false, "Music"))
        {
            // TODO: turn off the music
        }

        if (GUI.Toggle(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN + 40, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 2 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH - 40, BUTTON_HEIGHT), false, "SFX"))
        {
            // TODO: turn off the SFX
        }

        if (GUI.Button(new Rect((Screen.width - MENU_WIDTH) / 2 + MARIGIN, (Screen.height - MENU_HEIGHT) / 2 + MARIGIN + 3 * (BUTTON_HEIGHT + HORIZONTAL_BREAK), BUTTON_WIDTH, BUTTON_HEIGHT), "Back"))
        {
            currentGUIMethod = MainMenu;
        }
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
            currentGUIMethod = MainMenu;
        }
    }


}
