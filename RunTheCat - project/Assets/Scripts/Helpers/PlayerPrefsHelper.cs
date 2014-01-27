using UnityEngine;
using System.Collections;

public static class PlayerPrefsHelper
{
	#region highscores
	private static string levelKeyFormat = "Level{0}Highscore";

	/// <summary>
	/// 	Saves the score if it is higher
	/// 	than the previous one.
	/// </summary>
	public static void SaveLevelScore(int level, int score)
	{
		Debug.Log ("score: " + score);
		DebugScore(level);
		string key = string.Format(levelKeyFormat, level);
		if (PlayerPrefs.HasKey(key))
		{
			int lastHighScore = PlayerPrefs.GetInt(key);
			if(score > lastHighScore)
			{
				PlayerPrefs.SetInt(key, score);
				PlayerPrefs.Save();
			}
		}
		else
		{
			PlayerPrefs.SetInt(key, score);
			PlayerPrefs.Save();
		}
		DebugScore(level);
	}

	public static int? GetLevelHighScore(int level)
	{
		DebugScore (level);
		string key = string.Format(levelKeyFormat, level);
		if (PlayerPrefs.HasKey (key))
		{
			Debug.Log(PlayerPrefs.GetInt(key));
			return PlayerPrefs.GetInt (key);
		}
		else
			return null;
	}

	private static void DebugScore(int level)
	{
		string key = string.Format(levelKeyFormat, level);
		Debug.Log (PlayerPrefs.HasKey(key) ?
		           "score level " + level + " " + PlayerPrefs.GetInt(key)
		           : ("brak score level " + level));
	}
	#endregion highscore

	#region music	
	private static string musicKey = "music";

	public static void SaveMusicOn(bool isMusicOn)
	{
		PlayerPrefs.SetInt(musicKey, isMusicOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	public static bool? GetMusicOn()
	{
		if (PlayerPrefs.HasKey(musicKey))
			return PlayerPrefs.GetInt(musicKey) == 1;
		else
			return null;
	}
	#endregion music
}
