using UnityEngine;
using System.Collections;

public class BonusPowerUp : MonoBehaviour 
{
    public int PointsGotten = 100;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Player.instance.Score += PointsGotten;
                audio.Play();
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (Collider2D c in GetComponents<Collider2D>())
                    c.enabled = false;
            }
        }
    }
}
