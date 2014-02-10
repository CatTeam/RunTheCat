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
                Terrain.instance.minimumSpeed += speed;
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (Collider2D c in GetComponents<Collider2D>())
                    c.enabled = false;
                audio.Play();
                Debug.Log(string.Format("Speed delta: {0}, terrain new speed: {1}",
                                        speed, Terrain.instance.normalSpeed));
            }
        }
    }
}
