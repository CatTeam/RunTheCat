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
                //Player.instance.transform.FindChild("sprite").collider2D.enabled = true;
                Terrain.RestoreSpeed();
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
                //Player.instance.transform.FindChild("sprite").collider2D.enabled = false;
                Terrain.ChangeAndSaveSpeed(SPEED);
                this.gameObject.transform.FindChild("sprite").gameObject.SetActive(false);
            }
        }
    }
}
