using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
    public int PointsGotten = 100;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Player.instance.Score += PointsGotten;
                Debug.Log(Player.instance.Score);
            }
        }

    }
}
