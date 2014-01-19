using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector]
    public int Score;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        PlayerController.instance.RestartPosition();
    }


    public void GameOver()
    {
        //TODO: GameOver action.
        Respawn();// respawnuje sie dziwnie, do sprawdzenia/poprawienia jak będzie spiete z gui
    }
}
