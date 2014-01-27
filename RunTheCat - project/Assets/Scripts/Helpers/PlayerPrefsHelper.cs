using UnityEngine;
using System.Collections;

public static class PlayerPrefsHelper
{
    #region highscores
    private static string levelKeyFormat = "Level{0}Highscore";
    private static string finishedLevelsKey = "FinishedLevels";

    /// <summary>
    /// 	Saves the score if it is higher
    /// 	than the previous one.
    /// </summary>
    public static void SaveLevelScore(int level, int score)
    {
        // save level highscore
        string key = string.Format(levelKeyFormat, level);
        int lastHighScore = PlayerPrefs.GetInt(key, -1);
        if (score > lastHighScore)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }

        // save finished level #
        int lastFinishedLevel = PlayerPrefs.GetInt(finishedLevelsKey, -1);
        if (lastFinishedLevel < level)
        {
            PlayerPrefs.SetInt(finishedLevelsKey, level);
            PlayerPrefs.Save();
        }
    }

    public static int GetLevelHighScore(int level)
    {
        string key = string.Format(levelKeyFormat, level);
        return PlayerPrefs.GetInt(key, -1);
    }

    public static int GetFinishedLevelsNo()
    {
        return PlayerPrefs.GetInt(finishedLevelsKey, 0);
    }
    #endregion highscore

    #region music
    private static string musicKey = "Music";

    public static void SaveMusicOn(bool isMusicOn)
    {
        PlayerPrefs.SetInt(musicKey, isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetMusicOn()
    {
        return PlayerPrefs.GetInt(musicKey, 1) == 1;
    }
    #endregion music
}
