using UnityEngine;
using System.Collections;

public class DashPowerUp : MonoBehaviour
{
    public float DashLength = 100f;

    private bool isDashing = false;
    private const int SPEED = 50;

    // DOTO: implement invulnerability to obstacles while in dash

    void Update()
    {
        if (isDashing)
        {
            Debug.Log("Dash in progress");
            if (DashLength > 0)
            {
                DashLength -= Time.deltaTime * SPEED;
            }
            else
            {
                isDashing = false;
                Player.instance.transform.GetComponent<CircleCollider2D>().enabled = true;
                Terrain.RestoreSpeed();
                Player.instance.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                Debug.Log("Dash finished");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player" && !isDashing)
            {
                Debug.Log("Dash triggered");
                isDashing = true;
                Player.instance.transform.GetComponent<CircleCollider2D>().enabled = false;
                Terrain.ChangeAndSaveSpeed(SPEED);
                Player.instance.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerDashing";
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (Collider2D c in GetComponents<Collider2D>())
                    c.enabled = false;
                audio.Play();
            }
        }
    }
}
