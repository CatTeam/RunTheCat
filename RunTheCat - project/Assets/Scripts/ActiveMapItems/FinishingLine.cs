using UnityEngine;
using System.Collections;

public class FinishingLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Finished!");
            }
        }
    }
}
