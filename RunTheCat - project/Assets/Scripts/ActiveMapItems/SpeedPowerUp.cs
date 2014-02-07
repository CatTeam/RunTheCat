using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public int speed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Terrain.instance.normalSpeed += speed;
                gameObject.SetActive(false);
                Debug.Log(string.Format("Speed delta: {0}, terrain new speed: {1}",
                                        speed, Terrain.instance.normalSpeed));
            }
        }
    }
}
