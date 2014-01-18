using UnityEngine;
using System.Collections;

public class DashPowerUp : MonoBehaviour
{
    public int DashDistance = 100;
    public float DashSeconds = 2;
    private bool DashInProgress = false;
    private float TimePassed = 0;
    void Update()
    {
        if(DashInProgress)
        {
            Debug.Log("Dash in progress");
            if(TimePassed < DashSeconds)
            {
                float translation = - Time.deltaTime * DashDistance / DashSeconds;
                PlayerController.instance.Dash(translation);
                TimePassed += Time.deltaTime;
            }
            else
            {
                DashInProgress = false;
                TimePassed = 0;
                Player.instance.transform.FindChild("sprite").collider2D.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                DashInProgress = true;
                Player.instance.transform.FindChild("sprite").collider2D.enabled = false;
            }
        }
    }
}
