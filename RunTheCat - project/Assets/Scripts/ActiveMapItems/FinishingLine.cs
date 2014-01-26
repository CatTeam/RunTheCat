using UnityEngine;
using System.Collections;

public class FinishingLine : MonoBehaviour
{
	public int levelNo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Finished!");
                Player.instance.isLevelCompleted = true;
				PlayerPrefsHelper.SaveLevelScore(levelNo, Player.instance.Score);
				Debug.Log(PlayerPrefsHelper.GetLevelHighScore(levelNo));
				gameObject.SetActive(false);
            }
        }
    }
}
