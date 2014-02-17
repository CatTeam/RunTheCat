using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public float SpeedPercentage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Terrain.instance.normalSpeed *= SpeedPercentage;
                Terrain.instance.minimumSpeed *= SpeedPercentage;
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (Collider2D c in GetComponents<Collider2D>())
                    c.enabled = false;
                audio.Play();
                Debug.Log(string.Format("Speed delta: {0}, terrain new speed: {1}",
                                        SpeedPercentage, Terrain.instance.normalSpeed));
            }
        }
    }
}
