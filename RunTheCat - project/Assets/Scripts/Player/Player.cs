using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static int MAX_HP = 50;

    [HideInInspector]
    public double currentHp { get; set; }

    // Use this for initialization
    void Start()
    {
        currentHp = MAX_HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp < MAX_HP)
        {
            GameOver();   
        }
    }

    public void Respawn()
    {
        PlayerController.instance.RestartPosition();
        currentHp = MAX_HP;
    }

    public void GameOver()
    {
        //TODO: GameOver action.
    }
}
