using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour
{

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Enter ice");
                PlayerController.instance.playerState.isOnIce = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Leave ice");
                PlayerController.instance.playerState.isOnIce = false;
            }
        }
    }
}
