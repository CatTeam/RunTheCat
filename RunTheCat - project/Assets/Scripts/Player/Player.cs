using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector]
    public int Score;

	public bool isLevelFailed = false;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
